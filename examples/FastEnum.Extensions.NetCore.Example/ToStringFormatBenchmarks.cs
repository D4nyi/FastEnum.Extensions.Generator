using BenchmarkDotNet.Attributes;

namespace FastEnum.Extensions.NetCore.Example;

[MemoryDiagnoser]
public class ToStringFormatBenchmarks
{
    [Params(null, "g", "d", "x", "f")]
    public string? Formats { get; set; }

    [Params(Big.Value0, Big.Value512, Big.Value1023, (Big)2048)]
    public Big BenchmarkValue { get; set; }

    [Benchmark(Baseline = true)]
    public string ToString_Format_Original()
    {
        return BenchmarkValue.ToString(Formats);
    }

    [Benchmark]
    public string ToString_Format_Extension()
    {
        return BenchmarkValue.FastToString(Formats);
    }
}
