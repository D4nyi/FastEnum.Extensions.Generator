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

    private static object? Transform(GeneratorAttributeSyntaxContext context, CancellationToken ct)
    {
        if (context.TargetSymbol is not INamedTypeSymbol enumSymbol)
        {
            // nothing to do if this type isn't available
            return null;
        }

        ct.ThrowIfCancellationRequested();

        bool hasFlags = enumSymbol.GetAttributes().Any(static attributeData =>
            attributeData.AttributeDataIs(Constants.FlagsAttributeFullName));

        NestingState nestingState = enumSymbol.GetNesting();
        string modifier = enumSymbol.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Protected => "protected",
            Accessibility.Private => "private",
            Accessibility.ProtectedAndInternal or Accessibility.ProtectedOrInternal => "protected internal",
            _ => null!
        };

        if (enumSymbol.IsFileLocal)
        {
            modifier = "file";
        }

        string typeName = enumSymbol.ToString();

        Location location = enumSymbol.Locations.FirstOrDefault() ?? Location.None;

        Diagnostic? warning = HasNestingWarning(nestingState, modifier, location, typeName);

        if (warning is not null)
        {
            return warning;
        }

        if (Array.Exists(Constants.UnsupportedVisibilityModifiers, x => x == modifier))
        {
            return Diagnostic.Create(
                InvalidVisibilityModifier,
                location,
                typeName);
        }

        bool isGlobalNamespace = enumSymbol.ContainingNamespace.IsGlobalNamespace;
        string @namespace = enumSymbol.ContainingNamespace.ToString();

        string underlyingTypeName = enumSymbol.EnumUnderlyingType?.ToString()!;
        if (String.IsNullOrWhiteSpace(underlyingTypeName))
        {
            underlyingTypeName = "int";
        }

        ImmutableArray<EnumMemberSpec> members = enumSymbol.GetMembers()
            .Where(static x => x.Kind == SymbolKind.Field) // filter out ctor
            .Cast<IFieldSymbol>()
            .Select(x =>
            {
                AttributeValues data = x.GetAttributeValues();

                return new EnumMemberSpec(typeName, x.Name, x.ConstantValue!, data);
            })
            .OrderBy(x => x.Value)
            .ToImmutableArray();

        if (members.IsDefaultOrEmpty)
        {
            return Diagnostic.Create(EmptyEnum, location, typeName);
        }

        return new EnumGenerationSpec(
            typeName,
            modifier,
            members,
            isGlobalNamespace,
            @namespace,
            underlyingTypeName,
            hasFlags
        );
    }

    private static Diagnostic? HasNestingWarning(NestingState nestingState, string modifier, Location location, string name)
    {
        if (nestingState < NestingState.MultipleParentType)
        {
            return null;
        }

        if (nestingState.HasFlagFast(NestingState.GenericParentType))
        {
            return Diagnostic.Create(GenericParentType, location, name);
        }

        if (nestingState.HasFlagFast(NestingState.MultipleParentType))
        {
            return Diagnostic.Create(MultipleParentType, location, name);
        }

        if (nestingState.HasFlagFast(NestingState.InternalSingleParentType) && modifier != "internal")
        {
            return Diagnostic.Create(InconsistentAccessibilityWithParentType, location, name, modifier);
        }

        return null;
    }
}
