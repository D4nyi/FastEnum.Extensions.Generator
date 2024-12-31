using System.Collections.Immutable;
using System.Diagnostics;

using FastEnum.Extensions.Generator.Specs;
using FastEnum.Extensions.Generator.Utils;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static FastEnum.Extensions.Generator.EnumExtensionsGenerator;

namespace FastEnum.Extensions.Generator;

internal sealed class EnumExtensionsParser
{
    #region Warnings

    private static readonly DiagnosticDescriptor _invalidVisibilityModifier = new(
        id: "ETS1001",
        title: "Invalid visibility modifier",
        messageFormat: "Extension generation for {0} is disabled because it has an unsupported visibility modifier",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description:
        "Current generation strategy does not support extension generation for enums with `protected`, `protected internal` or `private` visibility modifiers.");

    private static readonly DiagnosticDescriptor _genericParentType = new(
        id: "ETS1002",
        title: "Invalid nesting type",
        messageFormat: "Extension generation for {0} is disabled because it is nested in a generic type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum outside a generic type.");

    private static readonly DiagnosticDescriptor _multipleParentType = new(
        id: "ETS1003",
        title: "Multiple nesting type",
        messageFormat: "Extension generation for {0} is disabled because it has multiple parent types",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum as a standalone type or reduce nesting.");

    private static readonly DiagnosticDescriptor _inconsistentAccessibilityWithParentType = new(
        id: "ETS1004",
        title: "Multiple nesting type",
        messageFormat: "Extension generation for {0} is disabled because its accessibility modifier is inconsistent with its parent's",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum as a standalone type or reduce nesting.");

    #endregion

    private readonly Compilation _compilation;
    private readonly EnumSourceGenerationContext _sourceGenerationContext;

    public EnumExtensionsParser(Compilation compilation, in EnumSourceGenerationContext sourceGenerationContext)
    {
        _compilation = compilation;
        _sourceGenerationContext = sourceGenerationContext;
    }

    public List<EnumGenerationSpec> GetGenerationSpec(
        ImmutableArray<EnumDeclarationSyntax> classDeclarationSyntaxList,
        CancellationToken cancellationToken)
    {
        INamedTypeSymbol? displayAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.DisplayAttributeFullName);
        INamedTypeSymbol? enumMemberAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.EnumMemberAttributeFullName);
        INamedTypeSymbol? descriptionAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.DescriptionAttributeFullName);
        INamedTypeSymbol? extensionsAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.ExtensionsAttributeFullName);
        INamedTypeSymbol? flagsAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.FlagsAttributeFullName);

        Debug.Assert(extensionsAttributeSymbol is not null);
        Debug.Assert(displayAttributeSymbol is not null);
        Debug.Assert(enumMemberAttributeSymbol is not null);
        Debug.Assert(descriptionAttributeSymbol is not null);
        Debug.Assert(flagsAttributeSymbol is not null);

        List<EnumGenerationSpec> enumToGenerateList = [];

        IEnumerable<IGrouping<SyntaxTree, EnumDeclarationSyntax>> grouped = classDeclarationSyntaxList.GroupBy(c => c.SyntaxTree);

        foreach (IGrouping<SyntaxTree, EnumDeclarationSyntax> group in grouped)
        {
            SyntaxTree syntaxTree = group.Key;
            SemanticModel compilationSemanticModel = _compilation.GetSemanticModel(syntaxTree);

            foreach (EnumDeclarationSyntax enumDeclarationSyntax in group)
            {
                cancellationToken.ThrowIfCancellationRequested();

                (bool hasExtensions, bool hasFlags) = HasExtensionsAttributeDefined(
                    enumDeclarationSyntax.AttributeLists,
                    compilationSemanticModel,
                    extensionsAttributeSymbol!,
                    flagsAttributeSymbol!,
                    cancellationToken);

                if (!hasExtensions)
                {
                    continue;
                }

                INamedTypeSymbol? contextTypeSymbol = compilationSemanticModel.GetDeclaredSymbol(enumDeclarationSyntax, cancellationToken);
                Debug.Assert(contextTypeSymbol is not null);

                NestingState nestingState = GetNesting(contextTypeSymbol!);
                string modifier = GetAccessModifier(enumDeclarationSyntax);

                if (nestingState > NestingState.SingleParentType)
                {
                    ReportNestingWarnings(
                        nestingState,
                        modifier,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);

                    continue;
                }

                if (Array.Exists(Constants.UnsupportedVisibilityModifiers, x => x == modifier))
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        _invalidVisibilityModifier,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);

                    continue;
                }

                string? underlyingTypeName = contextTypeSymbol!.EnumUnderlyingType?.ToString();
                if (String.IsNullOrWhiteSpace(underlyingTypeName))
                {
                    underlyingTypeName = "int";
                }

                string typeName = contextTypeSymbol.ToString();

                // Get all the members in the enum
                ImmutableArray<EnumMemberSpec> members = contextTypeSymbol.GetMembers()
                    .Where(x => x.Kind == SymbolKind.Field) // filter out ctor
                    .Cast<IFieldSymbol>()
                    .Select(x =>
                    {
                        AttributeValues data = x.ReadAttributeValues(displayAttributeSymbol!, enumMemberAttributeSymbol!, descriptionAttributeSymbol!);

                        return new EnumMemberSpec(typeName, x.Name, x.ConstantValue!, data);
                    })
                    .OrderBy(x => x.Value)
                    .ToImmutableArray();

                EnumGenerationSpec enumToGenerate = new(
                    typeName,
                    modifier,
                    members,
                    contextTypeSymbol.ContainingNamespace.ToString(),
                    underlyingTypeName!,
                    hasFlags
                );

                enumToGenerateList.Add(enumToGenerate);
            }
        }

        return enumToGenerateList;
    }

    private void ReportNestingWarnings(NestingState nestingState, string modifier, Location location, string name)
    {
        if (HasFlag(nestingState, NestingState.GenericParentType))
        {
            _sourceGenerationContext.ReportDiagnostic(
                _genericParentType,
                location,
                name);
        }

        if (HasFlag(nestingState, NestingState.MultipleParentType))
        {
            _sourceGenerationContext.ReportDiagnostic(
                _multipleParentType,
                location,
                name);
        }

        if (HasFlag(nestingState, NestingState.InternalSingleParentType) && modifier != "internal")
        {
            _sourceGenerationContext.ReportDiagnostic(
                _inconsistentAccessibilityWithParentType,
                location,
                name);
        }
    }

    private static (bool hasExtensions, bool hasFlags) HasExtensionsAttributeDefined(
        SyntaxList<AttributeListSyntax> attributeList,
        SemanticModel compilationSemanticModel,
        ISymbol extensionsAttribute,
        ISymbol flagsAttribute,
        CancellationToken cancellationToken)
    {
        if (attributeList.Count == 0)
        {
            return (false, false);
        }

        bool hasExtensions = false;
        bool hasFlags = false;

        foreach (AttributeSyntax? appliedAttribute in attributeList.SelectMany(x => x.Attributes))
        {
            SymbolInfo symbolInfo = compilationSemanticModel.GetSymbolInfo(appliedAttribute, cancellationToken);

            INamedTypeSymbol? attributeContainingTypeSymbol = symbolInfo.Symbol?.ContainingType;

            if (extensionsAttribute.Equals(attributeContainingTypeSymbol, SymbolEqualityComparer.Default))
            {
                hasExtensions = true;
            }
            else if (flagsAttribute.Equals(attributeContainingTypeSymbol, SymbolEqualityComparer.Default))
            {
                hasFlags = true;
            }
        }

        return (hasExtensions, hasFlags);
    }

    private static NestingState GetNesting(ISymbol typeSymbol)
    {
        INamedTypeSymbol containingType = typeSymbol.ContainingType;

        int nestingLevel = 0;
        NestingState nestingState = NestingState.Namespace;

        while (containingType is not null && containingType.Kind != SymbolKind.Namespace)
        {
            nestingLevel++;

            if (containingType is
                {
                    Kind: SymbolKind.NamedType,
                    TypeKind: TypeKind.Interface or TypeKind.Struct or TypeKind.Class,
                    TypeArguments.IsDefaultOrEmpty: false
                })
            {
                nestingState |= NestingState.GenericParentType;
            }

            containingType = containingType.ContainingType;
        }

        if (nestingLevel == 1)
        {
            nestingState |= NestingState.SingleParentType;

            Accessibility accessibility = typeSymbol.ContainingType.DeclaredAccessibility;
            if (accessibility != Accessibility.Public)
            {
                nestingState |= NestingState.InternalSingleParentType;
            }
        }
        else if (nestingLevel > 1)
        {
            nestingState |= NestingState.MultipleParentType;
        }

        return nestingState;
    }

    private static string GetAccessModifier(MemberDeclarationSyntax enumDeclarationSyntax) =>
        enumDeclarationSyntax.Modifiers.Count > 0 ? enumDeclarationSyntax.Modifiers[0].Text : "internal";

    private static Location GetLocation(ISymbol contextTypeSymbol) =>
        contextTypeSymbol.Locations.Length > 0 ? contextTypeSymbol.Locations[0] : Location.None;

    private static bool HasFlag(NestingState instance, NestingState flag) => (instance & flag) == flag;

    [Flags]
    private enum NestingState
    {
        Namespace = 0,
        SingleParentType = 1,
        MultipleParentType = 2,
        GenericParentType = 4,
        InternalSingleParentType = 8
    }
}
