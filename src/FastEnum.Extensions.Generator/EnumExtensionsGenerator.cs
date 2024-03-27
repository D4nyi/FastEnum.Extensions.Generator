using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FastEnum.Extensions.Generator;

[Generator]
public sealed partial class EnumExtensionsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(PostInit);

        //context.RegisterImplementationSourceOutput(
        //    context.AnalyzerConfigOptionsProvider,
        //    (_, options) => ExtractBuildProperties(options));

        IncrementalValuesProvider<EnumDeclarationSyntax> enumDeclarations =
            context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    Constants.ExtensionsAttributeFullName,
                    (node, _) => node is EnumDeclarationSyntax,
                    (syntaxContext, _) => (EnumDeclarationSyntax)syntaxContext.TargetNode
                );

        IncrementalValueProvider<(Compilation, ImmutableArray<EnumDeclarationSyntax>)> compilationAndEnums =
            context.CompilationProvider.Combine(enumDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndEnums,
            (spc, source) =>
                Execute(source.Item1, source.Item2, spc));
    }

    private static void Execute(
        Compilation compilation,
        in ImmutableArray<EnumDeclarationSyntax> contextClasses,
        in SourceProductionContext sourceProductionContext)
    {
        if (contextClasses.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        try
        {
            EnumSourceGenerationContext context = new(sourceProductionContext);
            EnumExtensionsParser parser = new(compilation, context);
            List<EnumGenerationSpec>? spec = parser.GetGenerationSpec(
                contextClasses,
                sourceProductionContext.CancellationToken);

            if (spec is null || spec.Count == 0) return;

            EnumExtensionsEmitter emitter = new(in context, spec);
            emitter.Emit();
        }
        catch (Exception e)
        {
            sourceProductionContext.AddSource("errors.txt", e.ToString());
            throw;
        }
    }

    private static void PostInit(IncrementalGeneratorPostInitializationContext ctx) =>
        ctx.AddSource(Constants.AttributesFile, SourceText.From(Constants.Attributes, Encoding.UTF8));

    //private void ExtractBuildProperties(AnalyzerConfigOptionsProvider options)
    //{
    //    //options.GlobalOptions.TryGetValue("build_property.FastEnumDefaultBehaviour", out string? defaultBehaviour);

    //    bool success = options.GlobalOptions.TryGetValue("build_property.targetframework", out string? targetFrameworks);
    //    if (success && !String.IsNullOrEmpty(targetFrameworks))
    //    {
    //        _targetFrameworks = ParseFrameworkVersion(targetFrameworks!);
    //        return;
    //    }

    //    success = options.GlobalOptions.TryGetValue("build_property.targetframeworks", out targetFrameworks);
    //    if (success && targetFrameworks is not null)
    //    {
    //        _targetFrameworks = ParseFrameworkVersions(targetFrameworks);
    //        return;
    //    }

    //    // if the target is NetFramework 4.8 then
    //    // there's no way of extracting that information
    //    _targetFrameworks = Framework.NetStandard20;
    //}

    //private static Framework ParseFrameworkVersion(string version)
    //{
    //    return version switch
    //    {
    //        "netFramework4.8" => Framework.NetFramework48,
    //        "netStandard2.0" => Framework.NetStandard20,
    //        "net6.0" or "net7.0" or "net8.0" or "net9.0" => Framework.NetCore3OrAbove,
    //        _ => Framework.NetStandard20,
    //    };
    //}

    //private static Framework ParseFrameworkVersions(string targetVersions)
    //{
    //    string[] versions = targetVersions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    //    if (versions.Length == 0)
    //    {
    //        return Framework.NetStandard20;
    //    }

    //    if (versions.Length == 1)
    //    {
    //        return ParseFrameworkVersion(versions[0]);
    //    }

    //    Framework framework = Framework.NetCore3OrAbove;
    //    foreach (string version in versions)
    //    {
    //        Framework temp = ParseFrameworkVersion(version);
    //        if (temp < framework)
    //        {
    //            framework = temp;
    //        }
    //    }

    //    return framework;
    //}

    internal readonly struct EnumSourceGenerationContext
    {
        private readonly SourceProductionContext _context;

        public EnumSourceGenerationContext(in SourceProductionContext context)
        {
            _context = context;
        }

        public void ReportDiagnostic(DiagnosticDescriptor descriptor, Location? location, params object?[]? args)
        {
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, location, args));
        }

        public void AddSource(string hintName, SourceText sourceText)
        {
            _context.AddSource(hintName, sourceText);
        }
    }
}

/*
    /// <summary>
    /// Choose the default behaviour of the `FastToString` method if no matching value is found.<br/>
    /// <strong>You may override the global default behaviour!</strong>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("FastEnum.Helpers.Generator.EnumToStringGenerator", "{{Assembly.Version}}")]
    public enum ToStringDefault
    {
        /// <summary>
        /// Like the built-in `ToString` will convert the enum to its numeric representation
        /// </summary>
        Default,
        /// <summary>
        /// The first value in the enum will be used if no matching value is found
        /// </summary>
        First,
        /// <summary>
        /// Will throw an <see cref="global::System.ArgumentOutOfRangeException"/> if no matching value is found
        /// </summary>
        Throw
    }
*/
