using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    // The source code to test
    private const string Source =
/*lang=csharp*/
"""
namespace SnapshotTesting
{
    [FastEnum.Extensions]
    public enum Color
    {
        [System.ComponentModel.Description("Crimson red")]
        Red = 0,
        [System.Runtime.Serialization.EnumMember(Value = "Pine")]
        Green = 1,
        [System.ComponentModel.DataAnnotations.Display(Name = "Sky", Description = "Sky")]
        Blue = 2,
    }
}
""";

    private static readonly string _snapshotDirectory = CreateDirectoryPath();
    private readonly GeneratorDriver _generatorDriver = CreateGeneratorDriver(Source);

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Pass the source code to our helper and snapshot test the output
        return Verify(_generatorDriver).UseDirectory(_snapshotDirectory);
    }

    [Fact]
    public Task RunResults()
    {
        GeneratorDriverRunResult runResults = _generatorDriver.GetRunResult();

        return Verify(runResults).UseDirectory(_snapshotDirectory);
    }

    [Fact]
    public Task RunResult()
    {
        GeneratorRunResult runResult = _generatorDriver.GetRunResult().Results.Single();

        return Verify(runResult).UseDirectory(_snapshotDirectory);
    }

    /// <summary>Create subdirectory for each .Net framework version</summary>
    private static string CreateDirectoryPath()
    {
        ReadOnlySpan<char> versionString = typeof(object).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        return versionString.IsEmpty ? "Snapshots/netX" : $"Snapshots/net{versionString[0]}";
    }

    private static GeneratorDriver CreateGeneratorDriver(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create references for assemblies we require
        // We could add multiple references if required
        PortableExecutableReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.ComponentModel.DescriptionAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.Serialization.EnumMemberAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute).Assembly.Location),
        ];

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "GeneratorTests",
            syntaxTrees: [syntaxTree],
            references: references); // pass the references to the compilation

        // Create an instance of our EnumGenerator incremental source generator
        EnumExtensionsGenerator generator = new();

        // The GeneratorDriver is used to run our generator against a compilation
        return CSharpGeneratorDriver.Create(generator)
            .RunGenerators(compilation);  // Run the source generator!
    }
}
