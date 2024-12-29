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
    #region Static & Const fields

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

        Debug.Assert(extensionsAttributeSymbol is not null);
        Debug.Assert(displayAttributeSymbol is not null);
        Debug.Assert(enumMemberAttributeSymbol is not null);
        Debug.Assert(descriptionAttributeSymbol is not null);

        List<EnumGenerationSpec> enumToGenerateList = [];

        IEnumerable<IGrouping<SyntaxTree, EnumDeclarationSyntax>> grouped = classDeclarationSyntaxList
            .GroupBy(c => c.SyntaxTree);

        foreach (IGrouping<SyntaxTree, EnumDeclarationSyntax> group in grouped)
        {
            SyntaxTree syntaxTree = group.Key;
            SemanticModel compilationSemanticModel = _compilation.GetSemanticModel(syntaxTree);

            foreach (EnumDeclarationSyntax enumDeclarationSyntax in group)
            {
                cancellationToken.ThrowIfCancellationRequested();

                bool isExtensionsDefined = HasExtensionsAttributeDefined(
                    enumDeclarationSyntax.AttributeLists,
                    compilationSemanticModel,
                    extensionsAttributeSymbol!,
                    cancellationToken);

                if (!isExtensionsDefined)
                {
                    // the type is not indicated with [Extensions]
                    continue;
                }

                INamedTypeSymbol? contextTypeSymbol =
                    compilationSemanticModel.GetDeclaredSymbol(enumDeclarationSyntax, cancellationToken);
                Debug.Assert(contextTypeSymbol is not null);

                Nesting nesting = GetNesting(contextTypeSymbol!);

                if (nesting > Nesting.SingleParentType)
                {
                    ReportNestingWarnings(nesting, GetLocation(contextTypeSymbol!), contextTypeSymbol!.Name);

                    continue;
                }

                string modifier = GetAccessModifier(enumDeclarationSyntax);
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
                        AttributeValues data = x.ReadAttributeValues(displayAttributeSymbol!,
                            enumMemberAttributeSymbol!, descriptionAttributeSymbol!);

                        return new EnumMemberSpec(typeName, x.Name, x.ConstantValue!, data);
                    })
                    .OrderBy(x => x.Value)
                    .ToImmutableArray();

                EnumGenerationSpec enumToGenerate = new(
                    typeName,
                    modifier,
                    members,
                    contextTypeSymbol.ContainingNamespace.ToString(),
                    underlyingTypeName!
                );

                enumToGenerateList.Add(enumToGenerate);
            }
        }

        return enumToGenerateList;
    }

    private void ReportNestingWarnings(Nesting nesting, Location location, string name)
    {
        if (HasFlag(nesting, Nesting.GenericParentType))
        {
            _sourceGenerationContext.ReportDiagnostic(
                _genericParentType,
                location,
                name);
        }

        if (HasFlag(nesting, Nesting.MultipleParentType))
        {
            _sourceGenerationContext.ReportDiagnostic(
                _multipleParentType,
                location,
                name);
        }
    }

    private static bool HasExtensionsAttributeDefined(
        SyntaxList<AttributeListSyntax> attributeList,
        SemanticModel compilationSemanticModel,
        ISymbol extensionsAttribute,
        CancellationToken cancellationToken)
    {
        if (attributeList.Count == 0)
        {
            return false;
        }

        IEnumerable<AttributeSyntax> attributes = attributeList.SelectMany(x => x.Attributes);

        foreach (AttributeSyntax? appliedAttribute in attributes)
        {
            SymbolInfo symbolInfo = compilationSemanticModel.GetSymbolInfo(appliedAttribute, cancellationToken);

            INamedTypeSymbol? attributeContainingTypeSymbol = symbolInfo.Symbol?.ContainingType;

            if (extensionsAttribute.Equals(attributeContainingTypeSymbol, SymbolEqualityComparer.Default))
            {
                return true;
            }
        }

        return false;
    }

    private static Nesting GetNesting(ISymbol typeSymbol)
    {
        INamedTypeSymbol containingType = typeSymbol.ContainingType;

        int nestingLevel = 0;
        Nesting nesting = Nesting.Namespace;

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
                nesting |= Nesting.GenericParentType;
            }

            containingType = containingType.ContainingType;
        }

        nesting |= nestingLevel < 2 ? Nesting.SingleParentType : Nesting.MultipleParentType;

        return nesting;
    }

    private static string GetAccessModifier(MemberDeclarationSyntax enumDeclarationSyntax) =>
        enumDeclarationSyntax.Modifiers.Count > 0 ? enumDeclarationSyntax.Modifiers[0].Text : "internal";

    private static Location GetLocation(ISymbol contextTypeSymbol) =>
        contextTypeSymbol.Locations.Length > 0 ? contextTypeSymbol.Locations[0] : Location.None;

    private static bool HasFlag(Nesting instance, Nesting flag) => (instance & flag) == flag;

    [Flags]
    private enum Nesting
    {
        Namespace = 0,
        SingleParentType = 1,
        MultipleParentType = 2,
        GenericParentType = 4,
    }
}
