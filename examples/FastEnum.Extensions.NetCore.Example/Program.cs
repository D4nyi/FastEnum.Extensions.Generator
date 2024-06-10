using BenchmarkDotNet.Running;
using FastEnum.Extensions.NetCore.Example;

var switcher = new BenchmarkSwitcher(new[]
{
    typeof(ToStringIsDefinedBenchmarks),
    typeof(ToStringFormatBenchmarks),
    typeof(TryParseBenchmarks),
    typeof(TryParseFlagsBenchmarks)
});

switcher.Run(args);
