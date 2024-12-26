using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;

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

    private const string NestedInClass =
        """
        namespace SnapshotTesting
        {
            public static class NestingClass
            {
                [FastEnum.Extensions]
                public enum NestedInClass
                {
                    None
                }
            }
        }
        """;

    private const string NestedInGenericClass =
        """
        namespace SnapshotTesting
        {
            public static class NestingGenericClass<T>
            {
                [FastEnum.Extensions]
                public enum NestedInGenericClass
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
            public static class NestingClass
            {
                [FastEnum.Extensions]
                private enum PrivateEnum
                {
                    None = 0
                }
            }
        }
        """;

    private const string EmptyEnum =
        """
        namespace SnapshotTesting
        {
            [FastEnum.Extensions]
            public enum EmptyEnum
            {
            }
        }
        """;

    private static readonly VerifySettings _verifySettings = new();

    private static readonly GeneratorDriver _generatorDriver = CreateGeneratorDriver();

    static EnumToStringGeneratorTests()
    {
        VerifierSettings.UseStrictJson();

        _verifySettings.UseStrictJson();
        _verifySettings.UseDirectory(CreateDirectoryPath());
    }

    [Fact]
    public Task GeneratesEnumExtensionsCorrectly()
    {
        return Verify(_generatorDriver, _verifySettings);
    }

    private static string CreateDirectoryPath()
    {
        const string folderPrefix = "Snapshots/net";

        ReadOnlySpan<char> versionString = typeof(object).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        return folderPrefix + (versionString.IsEmpty ? "X" : versionString[0].ToString());
    }

    private static GeneratorDriver CreateGeneratorDriver()
    {
        // Create a Roslyn compilation for the syntax trees.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "GeneratorTests",
            syntaxTrees:
            // Parse the provided string into a C# syntax tree
            [
                CSharpSyntaxTree.ParseText(Color),
                CSharpSyntaxTree.ParseText(GenerationOptions),
                CSharpSyntaxTree.ParseText(NestedInClass),
                CSharpSyntaxTree.ParseText(NestedInGenericClass),
                CSharpSyntaxTree.ParseText(PrivateEnum),
                CSharpSyntaxTree.ParseText(EmptyEnum)
            ],
            references:
            // Create references for assemblies we require
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DescriptionAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumMemberAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DisplayAttribute).Assembly.Location)
            ]);

        return CSharpGeneratorDriver
            .Create(new EnumExtensionsGenerator())
            .RunGenerators(compilation);
    }
}
