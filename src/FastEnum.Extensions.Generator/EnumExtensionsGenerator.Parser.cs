using System.Collections.Immutable;

using FastEnum.Extensions.Generator.Specs;
using FastEnum.Extensions.Generator.Utils;

using Microsoft.CodeAnalysis;

namespace FastEnum.Extensions.Generator;

public sealed partial class EnumExtensionsGenerator
{
    #region Warnings

    private static readonly DiagnosticDescriptor InvalidVisibilityModifier = new(
        id: "EEG1001",
        title: "Invalid visibility modifier",
        messageFormat: "Extension generation for {0} is disabled, because it has an unsupported visibility modifier",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Current generation strategy only supports extension generation for enums with `public` or `internal` visibility modifiers.",
        helpLinkUri: "https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1001-invalid-visibility-modifier",
        customTags: WellKnownDiagnosticTags.NotConfigurable);

    private static readonly DiagnosticDescriptor GenericParentType = new(
        id: "EEG1002",
        title: "Invalid nesting type",
        messageFormat: "Extension generation for {0} is disabled, because it is nested in a generic type",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum not inside a generic type.",
        helpLinkUri: "https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1002-invalid-backing-type");

    private static readonly DiagnosticDescriptor MultipleParentType = new(
        id: "EEG1003",
        title: "Multiple nesting type",
        messageFormat: "Extension generation for {0} is disabled, because it has multiple parent types",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum as a standalone type or reduce nesting.",
        helpLinkUri: "https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1003-invalid-nesting-type");

    private static readonly DiagnosticDescriptor InconsistentAccessibilityWithParentType = new(
        id: "EEG1004",
        title: "Multiple nesting type",
        messageFormat: "Extension generation for {0} is disabled, because its accessibility modifier ({1}) is inconsistent with its parent's",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Please define your enum as a standalone type or define the same accessibility modifier as its parent.",
        helpLinkUri: "https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1004-multiple-nesting-type");

    private static readonly DiagnosticDescriptor EmptyEnum = new(
        id: "EEG1005",
        title: "Enum has no defined members",
        messageFormat: "Extension generation for {0} is skipped, because it has no defined members",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Please define at least one member to get a generation output.",
        helpLinkUri: "https://github.com/D4nyi/FastEnum.Extensions.Generator/wiki/Analyzer-Rules#eeg1005-empty-enum");

    #endregion

    private static List<EnumBaseDataSpec> TransformExternal(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        List<EnumBaseDataSpec> enums = new();

        foreach (AttributeData attribute in context.Attributes)
        {
            ct.ThrowIfCancellationRequested();

            INamedTypeSymbol? attributeClass = attribute.AttributeClass;

            if (attributeClass is not { IsGenericType: true, TypeArguments.Length: 1, MetadataName: Constants.ExternalExtensionsAttributeShortName })
            {
                // wrong attribute
                continue;
            }

            if (attributeClass.TypeArguments[0] is not INamedTypeSymbol enumSymbol)
            {
                continue;
            }

            EnumBaseDataSpec? enumBaseData = TransformCore(enumSymbol);
            if (enumBaseData is not null)
            {
                enums.Add(enumBaseData.Value);
            }
        }

        return enums;
    }

    private static EnumBaseDataSpec? Transform(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        if (context.TargetSymbol is not INamedTypeSymbol enumSymbol)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        return TransformCore(enumSymbol);
    }

    private static EnumBaseDataSpec? TransformCore(INamedTypeSymbol enumSymbol)
    {
#pragma warning disable CA1307
#pragma warning disable CA1309 // No need for checking cultures and casing
        bool hasFlags = enumSymbol.GetAttributes().Any(static attributeData => Constants.FlagsAttributeName.Equals(attributeData.AttributeClass?.MetadataName));
#pragma warning restore CA1309
#pragma warning restore CA1307

        NestingState nestingState = enumSymbol.GetNesting();

        string modifier = enumSymbol.DeclaredAccessibility switch
        {
            Accessibility.Private => "private",
            Accessibility.ProtectedAndInternal or Accessibility.ProtectedOrInternal => "protected internal",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.Public => "public",
            _ => "file"
        };

        if (enumSymbol.IsFileLocal)
        {
            modifier = "file";
        }

        string typeName = enumSymbol.ToString();

        string underlyingTypeName = enumSymbol.EnumUnderlyingType?.ToString() ?? "int";

        (bool isGlobalNamespace, string ns) = GetNamespace(enumSymbol, nestingState);

        ImmutableArray<EnumFieldSpec> members = enumSymbol.GetMembers()
            .Where(static x => x.Kind == SymbolKind.Field) // filter out ctor
            .Cast<IFieldSymbol>()
            .Select(static x =>
            {
                AttributeInternalsSpec[] attributesData = x.GetAttributes()
                    .Select(y => new AttributeInternalsSpec(
                        y.AttributeClass?.MetadataName,
                        y.NamedArguments.ToDictionary(static x => x.Key, static x => x.Value),
                        y.ConstructorArguments.Length == 1 ? y.ConstructorArguments[0].Value : null
                    )).ToArray();

                return new EnumFieldSpec(x.Name, x.ConstantValue!, attributesData);
            })
            .ToImmutableArray();

        Location location = GetComparableLocation(enumSymbol.Locations.First());

        return new EnumBaseDataSpec(
            hasFlags,
            nestingState,
            modifier,
            typeName,
            underlyingTypeName,
            ns,
            isGlobalNamespace,
            members,
            location
        );
    }

    private static object CreateDiagnostics(EnumBaseDataSpec spec, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        Diagnostic? warning = HasNestingWarning(spec);

        if (warning is not null)
        {
            return warning;
        }

        if (Array.Exists(Constants.UnsupportedVisibilityModifiers, x => x == spec.Modifier))
        {
            return Diagnostic.Create(InvalidVisibilityModifier, spec.Location, spec.TypeName);
        }

        if (spec.Members.IsDefaultOrEmpty)
        {
            return Diagnostic.Create(EmptyEnum, spec.Location, spec.TypeName);
        }

        return spec;
    }

    private static object BuildGenerationSpec(object obj, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        if (obj is not EnumBaseDataSpec spec)
        {
            return obj;
        }

        ImmutableArray<EnumMemberSpec> members = spec.Members
            .Select(x =>
            {
                AttributeValues data = x.AttributesData.GetAttributeValues();

                return new EnumMemberSpec(spec.TypeName, x.Name, x.Value, data);
            })
            .OrderBy(x => x.Value)
            .ToImmutableArray();

        return new EnumGenerationSpec(
            spec.TypeName,
            spec.Modifier,
            members,
            spec.IsGlobalNamespace,
            spec.Namespace,
            spec.UnderlyingTypeName,
            spec.HasFlags
        );
    }

    private static Diagnostic? HasNestingWarning(EnumBaseDataSpec spec)
    {
        if (spec.NestingState < NestingState.MultipleParentType)
        {
            return null;
        }

        if (spec.NestingState.HasFlagFast(NestingState.GenericParentType))
        {
            return Diagnostic.Create(GenericParentType, spec.Location, spec.TypeName);
        }

        if (spec.NestingState.HasFlagFast(NestingState.MultipleParentType))
        {
            return Diagnostic.Create(MultipleParentType, spec.Location, spec.TypeName);
        }

        if (spec.NestingState.HasFlagFast(NestingState.InternalSingleParentType) && spec.Modifier != "internal")
        {
            return Diagnostic.Create(InconsistentAccessibilityWithParentType, spec.Location, spec.TypeName, spec.Modifier);
        }

        return null;
    }

    // Get a Location object that doesn't store a reference to the compilation.
    // That allows it to compare equally across compilations.
    private static Location GetComparableLocation(Location location)
    {
        string filePath = location.SourceTree?.FilePath ?? String.Empty;

        return Location.Create(filePath, location.SourceSpan, location.GetLineSpan().Span);

        // TextSpan sourceSpan = TextSpan.FromBounds(syntax.Modifiers.Span.Start, syntax.Identifier.Span.End);
        //
        // FileLinePositionSpan original = location.GetLineSpan();
        //
        // LinePosition startLinePosition = new(original.StartLinePosition.Line + 1, original.StartLinePosition.Character);
        // LinePosition endLinePosition = new(startLinePosition.Line, sourceSpan.Length);
        //
        // LinePositionSpan lineSpan = new(startLinePosition, endLinePosition);
        //
        //
        // return Location.Create(filePath, sourceSpan, lineSpan);
    }

    private static (bool isGlobalNamespace, string ns) GetNamespace(INamedTypeSymbol enumSymbol, NestingState nestingState)
    {
        if (nestingState == NestingState.SingleParentType)
        {
            enumSymbol = enumSymbol.ContainingType;
        }

        INamespaceSymbol ns = enumSymbol.ContainingNamespace;

        return (
            ns.IsGlobalNamespace,
            ns.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted))
        );
    }
}
