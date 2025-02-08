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
#pragma warning disable CA1307
        VerifySettings.ScrubLines(static x => x.Contains("[global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"FastEnum.Extensions.Generator.EnumExtensionsGenerator\", \""));
#pragma warning restore CA1307
    }

    internal static int ExpectedSourceCount(this Dictionary<string, string> enums)
    {
        // Analyzer warning raised for:
        // 'NestedInGenericClass', 'ProtectedClass', 'ProtectedInternalClass', 'PrivateClass', 'File',
        // 'NestedInMultipleClass', 'NestedInMultipleClassWithAGenericClass', 'EmptyEnum',
        // 'NestedWithInconsistentAccessibility', 'NestedInGenericClassWithInconsistentAccessibility'
        //
        // 10 removed because a warning
        // 'ExtensionsAttribute' is always added
        // -10 + 1 = -9

        return enums.Count - 9;
    }

    internal static CSharpCompilation CreateCompilation(Dictionary<string, string> enums)
    {
        CSharpParseOptions parseOptions = new(documentationMode: DocumentationMode.Diagnose);
        CSharpCompilationOptions compilationOptions = new(OutputKind.DynamicallyLinkedLibrary);

        string systemCoreLibLocation = typeof(object).Assembly.Location;
        string systemRuntimeLocation = GetSystemRuntimeLocation(systemCoreLibLocation);

        return CSharpCompilation.Create(
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
    }

    internal static GeneratorDriver CreateGeneratorDriver()
    {
        ISourceGenerator generator = new EnumExtensionsGenerator().AsSourceGenerator();

        // ⚠ Tell the driver to track all the incremental generator outputs
        // without this, you'll have no tracked outputs!
        GeneratorDriverOptions opts = new(
            disabledOutputs: IncrementalGeneratorOutputKind.None,
            trackIncrementalGeneratorSteps: true);

        return CSharpGeneratorDriver.Create([generator], driverOptions: opts);;
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
        int idx = systemCoreLibLocation.LastIndexOf(Path.DirectorySeparatorChar);

        return String.Concat(systemCoreLibLocation.AsSpan(0, idx + 1), "System.Runtime.dll");
    }
}
