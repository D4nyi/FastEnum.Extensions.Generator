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

        IncrementalValueProvider<ImmutableArray<object>> results = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Constants.ExtensionsAttributeFullName,
                predicate: static (node, _) => node is EnumDeclarationSyntax,
                transform: Transform)
            .WithTrackingName(Constants.InitialExtraction)
            .Where(static m => m is not null)
            .WithTrackingName(Constants.RemovingNulls)
            .Collect()
            .WithTrackingName(Constants.CollectedGenerationData)!

            // Apply sequence equality comparison on the result array for incremental caching.
            .WithComparer(new ObjectImmutableArraySequenceEqualityComparer<object>());

        context.RegisterSourceOutput(results, static (context, results) =>
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
            catch (Exception e)
            {
                context.AddSource("errors.txt", CreateSource(e.ToString()));
            }
        });
    }

    private static SourceText CreateSource(string sourceText) => SourceText.From(sourceText, Encoding.UTF8, SourceHashAlgorithm.Sha256);
}
