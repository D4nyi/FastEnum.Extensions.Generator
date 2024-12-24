using System.Collections.Immutable;
using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FastEnum.Extensions.Generator.Tests.Snapshot;

public sealed class EnumToStringGeneratorTests
{
    private const string Color =
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

    private const string GenerationOptions =
        """
        namespace SnapshotTesting
        {
            [FastEnum.Extensions, System.Flags]
            public enum GenerationOptions : byte
            {
                None = 0,
                [System.ComponentModel.Description("generate ToString")]
                ToString = 1,
                [System.Runtime.Serialization.EnumMember(Value = "generate Parse")]
                Parse = 2,
                [System.ComponentModel.DataAnnotations.Display(Name = "generate HasFlag", Description = "generate HasFlag")]
                HasFlag = 4
            }
        }
        """;

    private const string NestedInGenericClass =
        """
        namespace SnapshotTesting
        {
            public static class NestingClass<T>
            {
                [FastEnum.Extensions]
                public enum NestedInClass
                {
                    None
                }
            }

        }
        """;

    private const string PrivateEnum =
        """
        namespace SnapshotTesting
        {
            [FastEnum.Extensions]
            private enum GenerationOptions
            {
                None = 0
            }
        }
        """;

    private static readonly string _snapshotDirectory = CreateDirectoryPath();
    private static readonly GeneratorDriver _generatorDriver = CreateGeneratorDriver();

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        // Pass the source code to our helper and snapshot test the output
        return Verify(_generatorDriver).UseDirectory(_snapshotDirectory);
    }

    [Fact]
    public Task RunResult()
    {
        ImmutableArray<GeneratorRunResult> results = _generatorDriver.GetRunResult().Results;

        _ = Assert.Single(results);

        return Verify(results[0]).UseDirectory(_snapshotDirectory);
    }

    private static string CreateDirectoryPath()
    {
        ReadOnlySpan<char> versionString = typeof(object).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        return versionString.IsEmpty ? "Snapshots/netX" : $"Snapshots/net{versionString[0]}";
    }

    private static GeneratorDriver CreateGeneratorDriver()
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree colorSyntaxTree = CSharpSyntaxTree.ParseText(Color);
        SyntaxTree generationOptionsSyntaxTree = CSharpSyntaxTree.ParseText(GenerationOptions);
        SyntaxTree nestedInGenericClassSyntaxTree = CSharpSyntaxTree.ParseText(NestedInGenericClass);
        SyntaxTree privateEnumSyntaxTree = CSharpSyntaxTree.ParseText(PrivateEnum);

        // Create references for assemblies we require
        // We could add multiple references if required
        PortableExecutableReference[] references = [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.ComponentModel.DescriptionAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.Serialization.EnumMemberAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute).Assembly.Location),
        ];

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "GeneratorTests",
            syntaxTrees:
            [
                colorSyntaxTree,
                generationOptionsSyntaxTree,
                nestedInGenericClassSyntaxTree,
                privateEnumSyntaxTree
            ],
            references: references); // pass the references to the compilation

        // Create an instance of our EnumGenerator incremental source generator
        EnumExtensionsGenerator generator = new();

        // The GeneratorDriver is used to run our generator against a compilation
        return CSharpGeneratorDriver.Create(generator)
            .RunGenerators(compilation); // Run the source generator!
    }
}
