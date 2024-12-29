using System.Collections.Immutable;
using System.Text;

using FastEnum.Extensions.Generator.Specs;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FastEnum.Extensions.Generator;

[Generator]
public sealed class EnumExtensionsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //if (!System.Diagnostics.Debugger.IsAttached)
        //{
        //    System.Diagnostics.Debugger.Launch();
        //}

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
            List<EnumGenerationSpec> spec = parser.GetGenerationSpec(
                contextClasses,
                sourceProductionContext.CancellationToken);

            if (spec.Count == 0) return;

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

    internal readonly struct EnumSourceGenerationContext(in SourceProductionContext context)
    {
        private readonly SourceProductionContext _context = context;

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
