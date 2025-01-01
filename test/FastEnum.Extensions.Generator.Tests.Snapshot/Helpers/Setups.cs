using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Xunit.Abstractions;

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

    internal static int ExpectedSourceCount(this Dictionary<string, string> enums)
    {
        // Analyzer warning raised for:
        // 'NestedInGenericClass', 'ProtectedClass', 'ProtectedInternalClass', 'PrivateClass', 'File'
        // 'NestedInMultipleClass', 'NestedInMultipleClassWithAGenericClass',
        // 'NestedWithInconsistentAccessibility', 'NestedInGenericClassWithInconsistentAccessibility'
        //
        // 9 removed because a warning
        // 'ExtensionsAttribute' is always added
        // -9 + 1 = -8

        return enums.Count - 8;
    }

    internal static GeneratorDriver CreateGeneratorDriver(Dictionary<string, string> enums, ITestOutputHelper output)
    {
        CSharpParseOptions parseOptions = new(documentationMode: DocumentationMode.Diagnose);
        CSharpCompilationOptions compilationOptions = new(OutputKind.ConsoleApplication);

        string systemCoreLibLocation = typeof(object).Assembly.Location;
        string systemRuntimeLocation = GetSystemRuntimeLocation(systemCoreLibLocation);

        output.WriteLine("ASDTEST:");
        output.WriteLine(systemCoreLibLocation);
        output.WriteLine(systemRuntimeLocation);
        output.WriteLine(typeof(DescriptionAttribute).Assembly.Location);
        output.WriteLine(typeof(EnumMemberAttribute).Assembly.Location);
        output.WriteLine(typeof(DisplayAttribute).Assembly.Location);

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
                MetadataReference.CreateFromFile(systemCoreLibLocation),
                MetadataReference.CreateFromFile(systemRuntimeLocation),
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

    private static string GetSystemRuntimeLocation(string systemCoreLibLocation)
    {
        int idx = systemCoreLibLocation.LastIndexOf('\\');

        return String.Concat(systemCoreLibLocation.AsSpan(0, idx + 1), "System.Runtime.dll");
    }
}
