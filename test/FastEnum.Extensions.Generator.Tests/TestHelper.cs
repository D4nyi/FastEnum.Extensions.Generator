using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using FastEnum.Extensions.Generator;

namespace FastEnumToString.Tests;

internal static class TestHelper
{
    internal static Task Verify(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create references for assemblies we require
        // We could add multiple references if required
        IEnumerable<PortableExecutableReference> references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references); // pass the references to the compilation

        // Create an instance of our EnumGenerator incremental source generator
        EnumExtensionsGenerator generator = new EnumExtensionsGenerator();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier
            .Verify(driver)
            .UseDirectory("Snapshots");
    }
}
