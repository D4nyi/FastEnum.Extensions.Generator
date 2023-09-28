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

    private static readonly DiagnosticDescriptor _enumCannotBePrivate = new(
        id: "ETS1001",
        title: "Enum cannot be private",
        messageFormat: "For private enums we are currently unable to create extensions",
        category: Constants.FastEnumToStringGenerator,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _enumUnderlyingTypeCannotBeDetermined = new(
        id: "ETS1002",
        title: "Enum's underlying type cannot be determined",
        messageFormat: "Enum's underlying type cannot be determined therefore we are unable to create extensions",
        category: Constants.FastEnumToStringGenerator,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor _genericTypeNestingRestrictions = new(
        id: "ETS1003",
        title: "Extension generation restriction",
        messageFormat: "Extension generation for enum's nested in generic types are restricted",
        category: Constants.FastEnumToStringGenerator,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

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
                
                INamedTypeSymbol? contextTypeSymbol = compilationSemanticModel.GetDeclaredSymbol(enumDeclarationSyntax, cancellationToken);
                Debug.Assert(contextTypeSymbol is not null);

                if (IsNestedInGenericType(contextTypeSymbol!))
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        _genericTypeNestingRestrictions,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);
                    continue;
                }

                string modifier = GetAccessModifier(enumDeclarationSyntax);
                if (modifier == Constants.PrivateAccessModifier)
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        _enumCannotBePrivate,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);
                    continue;
                }

                string? underlyingTypeName = contextTypeSymbol!.EnumUnderlyingType?.ToString();
                if (String.IsNullOrEmpty(underlyingTypeName))
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        _enumUnderlyingTypeCannotBeDetermined,
                        GetLocation(contextTypeSymbol),
                        contextTypeSymbol.Name);
                    continue;
                }

                string typeName = contextTypeSymbol.ToString();

                // Get all the members in the enum
                ImmutableArray<EnumMemberSpec> members = contextTypeSymbol.GetMembers()
                    .Where(x => x.Kind == SymbolKind.Field) // filter out ctor
                    .Cast<IFieldSymbol>()
                    .Select(x =>
                    {
                        AttributeValues data = x.ReadAttributeValues(displayAttributeSymbol!, enumMemberAttributeSymbol! ,descriptionAttributeSymbol!);
                        
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

        var attributes = attributeList.SelectMany(x => x.Attributes);
        foreach (var appliedAttribute in attributes)
        {
            var symbolInfo = compilationSemanticModel.GetSymbolInfo(appliedAttribute, cancellationToken);
            if (symbolInfo.Symbol is not IMethodSymbol attributeSymbol)
            {
                continue;
            }

            INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
            if (extensionsAttribute.Equals(attributeContainingTypeSymbol, SymbolEqualityComparer.Default))
            {
                return appliedAttribute is not null;
            }
        }

        return false;
    }

    private static bool IsNestedInGenericType(ISymbol typeSymbol)
    {
        INamedTypeSymbol containingType = typeSymbol.ContainingType;

        bool isNestedInGenericType = false;
        while (containingType is not null && containingType.Kind != SymbolKind.Namespace)
        {
            if (containingType is
                { Kind: SymbolKind.NamedType, TypeKind: TypeKind.Interface or TypeKind.Struct or TypeKind.Class })
            {
                isNestedInGenericType |= !containingType.TypeArguments.IsDefaultOrEmpty;
            }

            containingType = containingType.ContainingType;
        }

        return isNestedInGenericType;
    }

    private static string GetAccessModifier(MemberDeclarationSyntax enumDeclarationSyntax) =>
        enumDeclarationSyntax.Modifiers.Count > 0 ? enumDeclarationSyntax.Modifiers[0].Text : "internal";

    private static Location GetLocation(ISymbol contextTypeSymbol) =>
        contextTypeSymbol.Locations.Length > 0 ? contextTypeSymbol.Locations[0] : Location.None;
}
