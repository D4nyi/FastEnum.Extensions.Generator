using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("FastEnum.Extensions.Generator.Tests.Snapshot")]

namespace FastEnum.Extensions.Generator;

internal static class Stages
{
    internal const string InitialExtraction = nameof(InitialExtraction);
    internal const string RemovingNulls = nameof(RemovingNulls);
    internal const string RemovingEmptyLists = nameof(RemovingEmptyLists);
    internal const string CreateDiagnostics = nameof(CreateDiagnostics);
    internal const string BuildGenerationSpec = nameof(BuildGenerationSpec);
    internal const string CollectedGenerationData = nameof(CollectedGenerationData);
}
