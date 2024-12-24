using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Running;

using FastEnum.Extensions.NetCore.Example;

[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Scope = "module")]
[assembly: SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Scope = "module")]

BenchmarkSwitcher switcher = new([
    typeof(ToStringIsDefinedBenchmarks),
    typeof(ToStringFormatBenchmarks),
    typeof(TryParseBenchmarks),
    typeof(TryParseFlagsBenchmarks)
]);

switcher.Run(args);
