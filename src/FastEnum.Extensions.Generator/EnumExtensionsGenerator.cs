using System.Collections.Immutable;
using System.Text;

using FastEnum.Extensions.Generator.Specs;
using FastEnum.Extensions.Generator.Utils;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FastEnum.Extensions.Generator;

[Generator]
public sealed partial class EnumExtensionsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
            ctx.AddSource(Constants.AttributesFile, CreateSource(Constants.Attributes)));

        IncrementalValueProvider<ImmutableArray<object>> ownedEnums = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Constants.ExtensionsAttributeFullName,
                predicate: static (node, _) => node is EnumDeclarationSyntax,
                transform: Transform)
            .WithTrackingName(Stages.InitialExtraction)
            .Where(static m => m.HasValue)
            .Select(static (x, _) => x!.Value)
            .WithTrackingName(Stages.RemovingNulls)
            .Select(CreateDiagnostics)
            .WithTrackingName(Stages.CreateDiagnostics)
            .Select(BuildGenerationSpec)
            .WithTrackingName(Stages.BuildGenerationSpec)
            .Collect()
            .WithTrackingName(Stages.CollectedGenerationData)

            // Apply sequence equality comparison on the result array for incremental caching.
            .WithComparer(new ObjectImmutableArraySequenceEqualityComparer<object>());

        IncrementalValueProvider<ImmutableArray<object>> externalEnums = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Constants.ExternalExtensionsAttributeFullName,
                predicate: static (node, _) => node is CompilationUnitSyntax,
                transform: TransformExternal)
            .WithTrackingName(Stages.InitialExtraction)
            .Where(static m => m.Count > 0)
            .SelectMany(static (x, _) => x)
            .WithTrackingName(Stages.RemovingEmptyLists)
            .Select(CreateDiagnostics)
            .WithTrackingName(Stages.CreateDiagnostics)
            .Select(BuildGenerationSpec)
            .WithTrackingName(Stages.BuildGenerationSpec)
            .Collect()
            .WithTrackingName(Stages.CollectedGenerationData)

            // Apply sequence equality comparison on the result array for incremental caching.
            .WithComparer(new ObjectImmutableArraySequenceEqualityComparer<object>());

        context.RegisterImplementationSourceOutput(ownedEnums, Execute);
        context.RegisterImplementationSourceOutput(externalEnums, Execute);
    }

    private static SourceText CreateSource(string sourceText) => SourceText.From(sourceText, Encoding.UTF8, SourceHashAlgorithm.Sha256);

    private static void  Execute(SourceProductionContext context, ImmutableArray<object> results)
    {
        if (results.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        try
        {
            foreach (object result in results)
            {
                switch (result)
                {
                    case Diagnostic d:
                        context.ReportDiagnostic(d);
                        break;
                    case EnumGenerationSpec enumGenerationSpec:
                        {
                            string generatedClass = Emit(enumGenerationSpec);
                            context.AddSource($"{enumGenerationSpec.Name}Extensions.g.cs", CreateSource(generatedClass));
                        }
                        break;
                }
            }
        }
#pragma warning disable CA1031
        catch (Exception e)
#pragma warning restore CA1031
        {
            context.AddSource("Errors.g.cs", CreateSource("// " + e.Message));
        }
    }
}
