using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FastEnum.Extensions.Generator.Tests.Snapshot.Helpers;

internal static class Setups
{
    internal static readonly VerifySettings VerifySettings = new();

    static Setups()
    {
        VerifierSettings.UseStrictJson();

        VerifySettings.UseStrictJson();
        VerifySettings.UseDirectory(CreateDirectoryPath());
    }

    internal static bool HasCorrectSourceCount(this GeneratorRunResult result, int enumsCount)
    {
        // Analyzer warning raised for:
        // 'NestedInGenericClass', 'ProtectedClass', 'ProtectedInternalClass', 'PrivateClass', 'File'
        // 'NestedInMultipleClass', 'NestedInMultipleClassWithAGenericClass'
        //
        // 7 removed because a warning
        // 'ExtensionsAttribute' is always added
        // -7 + 1 = -6

        return result.GeneratedSources.Length == (enumsCount - 6);
    }

    internal static GeneratorDriver CreateGeneratorDriver(Dictionary<string, string> enums)
    {
        CSharpParseOptions parseOptions = new(documentationMode: DocumentationMode.Diagnose);
        CSharpCompilationOptions compilationOptions = new(OutputKind.DynamicallyLinkedLibrary);

        // Create a Roslyn compilation for the syntax trees.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "GeneratorTests",
            options: compilationOptions,
            // Parse the provided string into a C# syntax tree
            syntaxTrees: enums.Select(x =>
                CSharpSyntaxTree.ParseText(x.Value, parseOptions, $"SnapshotTesting/{x.Key}.cs", Encoding.UTF8)),
            // Create references for assemblies we require
            references:
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

    private static string CreateDirectoryPath()
    {
        const string folderPrefix = "Snapshots/net";

        ReadOnlySpan<char> versionString = typeof(object).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;

        return folderPrefix + (versionString.IsEmpty ? "X" : versionString[0].ToString());
    }
}
