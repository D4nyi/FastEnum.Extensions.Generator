using BenchmarkDotNet.Attributes;

namespace ToStringExample;

[MemoryDiagnoser]
public class Benchmarks
{

    [Params(Big.Value0, Big.Value512, Big.Value1023, (Big)2048)]
    public Big BenchmarkValue { get; set; }

    [Benchmark]
    public bool IsDefined_Original()
    {
        return Enum.IsDefined(BenchmarkValue);
    }

    [Benchmark]
    public bool IsDefined_Extension()
    {
        return BenchmarkValue.IsDefined();
    }

    [Benchmark]
    public string ToString_Original()
    {
        return BenchmarkValue.ToString();
    }

    [Benchmark]
    public string ToString_Extension()
    {
        return BenchmarkValue.FastToString();
    }
}
