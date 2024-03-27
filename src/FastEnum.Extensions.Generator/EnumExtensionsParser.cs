using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
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

    private static readonly DiagnosticDescriptor EnumCannotBePrivate = new(
        id: "ETS1001",
        title: "Enum cannot be private",
        messageFormat: "For private enums we are currently unable to create extensions",
        category: Constants.FastEnumToStringGenerator,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor EnumUnderlyingTypeCannotBeDetermined = new(
        id: "ETS1002",
        title: "Enum's underlying type cannot be determined",
        messageFormat: "Enum's underlying type cannot be determined therefore we are unable to create extensions",
        category: Constants.FastEnumToStringGenerator,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor GenericTypeNestingRestrictions = new(
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
        INamedTypeSymbol? extensionsAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.ExtensionsAttributeFullName);
        INamedTypeSymbol? flagsAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.FlagsAttributeFullName);
        INamedTypeSymbol? descriptionAttributeSymbol = _compilation.GetTypeByMetadataName(Constants.DescriptionAttributeFullName);

        Debug.Assert(extensionsAttributeSymbol is not null);
        Debug.Assert(flagsAttributeSymbol is not null);
        Debug.Assert(descriptionAttributeSymbol is not null);

        List<EnumGenerationSpec> enumToGenerateList = new List<EnumGenerationSpec>();

        IEnumerable<IGrouping<SyntaxTree, EnumDeclarationSyntax>> grouped = classDeclarationSyntaxList
            .GroupBy(c => c.SyntaxTree);

        foreach (IGrouping<SyntaxTree, EnumDeclarationSyntax> group in grouped)
        {
            SyntaxTree syntaxTree = group.Key;
            SemanticModel compilationSemanticModel = _compilation.GetSemanticModel(syntaxTree);
            //CompilationUnitSyntax compilationUnitSyntax = (CompilationUnitSyntax)syntaxTree.GetRoot(cancellationToken);

            foreach (EnumDeclarationSyntax enumDeclarationSyntax in group)
            {
                cancellationToken.ThrowIfCancellationRequested();

                (AttributeSyntax? Extensions, bool HasFlags) attributeSyntax = GetToStringAttribute(
                    enumDeclarationSyntax.AttributeLists,
                    compilationSemanticModel,
                    extensionsAttributeSymbol!,
                    flagsAttributeSymbol!,
                    cancellationToken);

                if (attributeSyntax.Extensions is null)
                {
                    // the type is not indicated with [Extensions]
                    continue;
                }
                
                INamedTypeSymbol? contextTypeSymbol =
                    compilationSemanticModel.GetDeclaredSymbol(enumDeclarationSyntax, cancellationToken);
                Debug.Assert(contextTypeSymbol is not null);

                if (IsNestedInGenericType(contextTypeSymbol!))
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        GenericTypeNestingRestrictions,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);
                    continue;
                }

                string modifier = GetAccessModifier(enumDeclarationSyntax);
                if (modifier == Constants.PrivateAccessModifier)
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        EnumCannotBePrivate,
                        GetLocation(contextTypeSymbol!),
                        contextTypeSymbol!.Name);
                    continue;
                }

                string? underlyingTypeName = contextTypeSymbol!.EnumUnderlyingType?.ToString();
                if (String.IsNullOrEmpty(underlyingTypeName))
                {
                    _sourceGenerationContext.ReportDiagnostic(
                        EnumUnderlyingTypeCannotBeDetermined,
                        GetLocation(contextTypeSymbol),
                        contextTypeSymbol.Name);
                    continue;
                }

                string name = contextTypeSymbol.ToString();

                // Get all the members in the enum
                ImmutableArray<EnumMemberSpec> members = contextTypeSymbol.GetMembers()
                    .Where(x => x.Kind == SymbolKind.Field) // filter out ctor
                    .Cast<IFieldSymbol>()
                    .Select(x =>
                    {
                        var desc = x.ReadDescriptionValue(descriptionAttributeSymbol!);
                        
                        return new EnumMemberSpec(name, x.Name, x.ConstantValue!, desc);
                    })
                    .ToImmutableArray();
                
                EnumGenerationSpec enumToGenerate = new(
                    name,
                    modifier,
                    members,
                    contextTypeSymbol.ContainingNamespace.ToString(),
                    underlyingTypeName!,
                    attributeSyntax.HasFlags
                );

                enumToGenerateList.Add(enumToGenerate);
            }
        }

        return enumToGenerateList;
    }
    
    private static (AttributeSyntax? Extensions, bool HasFlags) GetToStringAttribute(
        SyntaxList<AttributeListSyntax> attributeList,
        SemanticModel compilationSemanticModel,
        ISymbol extensionsAttribute,
        ISymbol flagsAttributeSymbol,
        CancellationToken cancellationToken)
    {
        (AttributeSyntax? Extensions, bool HasFlags) result = (null, false);

        if (attributeList.Count == 0)
        {
            return result;
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
                result.Extensions = appliedAttribute;
            }
            else if (flagsAttributeSymbol.Equals(attributeContainingTypeSymbol, SymbolEqualityComparer.Default))
            {
                result.HasFlags = true;
            }
        }

        return result;
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

    private static string GenerateGenericTypeParameters(ISymbol typeSymbol)
    {
        INamedTypeSymbol? containingType = typeSymbol.ContainingType;
        if (containingType is null || containingType.TypeArguments.IsDefaultOrEmpty)
        {
            return "";
        }

        ImmutableArray<ITypeSymbol> typeArguments = containingType.TypeArguments;
        if (typeArguments.Length == 1)
        {
            return $"<{typeArguments[0].Name}>";
        }

        StringBuilder sb = StringBuilderPool.Get();
        sb.Append('<');

        bool first = true;
        foreach (ITypeSymbol typeArg in typeArguments)
        {
            if (!first)
            {
                sb.Append(", ");
            }
            else
            {
                first = false;
            }

            sb.Append(typeArg.Name);
        }

        sb.Append('>');

        return StringBuilderPool.Return(sb);
    }

    private static string GetGenericTypeConstraints(SyntaxNode enumDeclaration)
    {
        const int maxDepth = 3;
        int i = 0;
        SyntaxNode? parentDeclaration = enumDeclaration.Parent;
        while (parentDeclaration is not ClassDeclarationSyntax && i < maxDepth)
        {
            parentDeclaration = parentDeclaration?.Parent;
            i++;
        }

        if (parentDeclaration is null)
        {
            return " ";
        }

        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)parentDeclaration;

        string constraints = classDeclaration.ConstraintClauses.ToString();

        if (String.IsNullOrEmpty(constraints))
        {
            return " ";
        }

        string[] constraintsArr =
            constraints.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        if (constraintsArr.Length == 1)
        {
            return $" {constraints} ";
        }

        StringBuilder sb = StringBuilderPool.Get().AppendLine();
        const string constraintsIndent = "            ";
        const string switchIndent = "        ";

        foreach (string constraint in constraintsArr.Select(x => x.Trim()))
        {
            sb.Append(constraintsIndent).AppendLine(constraint);
        }

        sb.Append(switchIndent);

        return StringBuilderPool.Return(sb);
    }

    private static int GetToStringArgumentValue(
        AttributeSyntax attributeSyntax,
        SemanticModel compilationSemanticModel,
        CancellationToken cancellationToken)
    {
        if (!HasArgument(attributeSyntax))
        {
            return -2;
        }

        AttributeArgumentSyntax overrideArg = attributeSyntax.ArgumentList!.Arguments[0];
        ExpressionSyntax overrideExpr = overrideArg.Expression;
        Optional<object?> routeTemplate =
            compilationSemanticModel.GetConstantValue(overrideExpr, cancellationToken);
        if (!routeTemplate.HasValue || routeTemplate.Value is null ||
            !Int32.TryParse(routeTemplate.Value.ToString(), out int overridenDefault))
        {
            return -2;
        }

        return overridenDefault;
    }

    private static string GetAccessModifier(MemberDeclarationSyntax enumDeclarationSyntax) =>
        enumDeclarationSyntax.Modifiers.Count > 0 ? enumDeclarationSyntax.Modifiers[0].Text : "internal";

    private static Location GetLocation(ISymbol contextTypeSymbol) =>
        contextTypeSymbol.Locations.Length > 0 ? contextTypeSymbol.Locations[0] : Location.None;

    private static bool HasArgument(AttributeSyntax? syntax) =>
        syntax?.ArgumentList is not null && syntax.ArgumentList.Arguments.Count > 0;
}
