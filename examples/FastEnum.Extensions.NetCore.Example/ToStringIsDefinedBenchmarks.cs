using BenchmarkDotNet.Attributes;

namespace FastEnum.Extensions.NetCore.Example;

[MemoryDiagnoser]
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
public class ToStringIsDefinedBenchmarks
{
    [Params(Big.Value0, Big.Value512, Big.Value1023, (Big)2048)]
    public Big BenchmarkValue { get; set; }

    [Benchmark(Baseline = true), BenchmarkCategory("IsDefined")]
    public bool IsDefined_Original()
    {
        return Enum.IsDefined(BenchmarkValue);
    }

    [Benchmark, BenchmarkCategory("IsDefined")]
    public bool IsDefined_Extension()
    {
        return BenchmarkValue.IsDefined();
    }

    [Benchmark(Baseline = true), BenchmarkCategory("ToString")]
    public string ToString_Original()
    {
        return BenchmarkValue.ToString();
    }

    [Benchmark, BenchmarkCategory("ToString")]
    public string ToString_Extension()
    {
        return BenchmarkValue.FastToString();
    }
}
