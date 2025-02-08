using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace FastEnum.Extensions.NetCore.Example;

[MemoryDiagnoser]
[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class TryParseBenchmarks
{
    [Params(null, nameof(Big.Value0), nameof(Big.Value512), nameof(Big.Value1023), "2048")]
    public string? BenchmarkValue { get; set; }

    [Benchmark(Baseline = true), BenchmarkCategory("TryParse")]
    public bool TryParse_Original()
    {
        return Enum.TryParse(BenchmarkValue, out Big big);
    }

    [Benchmark, BenchmarkCategory("TryParse")]
    public bool TryParse_Extension()
    {
        return BigExtensions.TryParse(BenchmarkValue, out Big big);
    }

    [Benchmark(Baseline = true), BenchmarkCategory("TryParseSpan")]
    public bool TryParseSpan_Original()
    {
        return Enum.TryParse(BenchmarkValue.AsSpan(), out Big big);
    }

    [Benchmark, BenchmarkCategory("TryParseSpan")]
    public bool TryParseSpan_Extension()
    {
        return BigExtensions.TryParse(BenchmarkValue.AsSpan(), out Big big);
    }

    [Benchmark(Baseline = true), BenchmarkCategory("TryParseIgnoreCase")]
    public bool TryParseIgnoreCase_Original()
    {
        return Enum.TryParse(BenchmarkValue, true, out Big big);
    }

    [Benchmark, BenchmarkCategory("TryParseIgnoreCase")]
    public bool TryParseIgnoreCase_Extension()
    {
        return BigExtensions.TryParseIgnoreCase(BenchmarkValue, out Big big);
    }

    [Benchmark(Baseline = true), BenchmarkCategory("TryParseSpanIgnoreCase")]
    public bool TryParseSpanIgnoreCase_Original()
    {
        return Enum.TryParse(BenchmarkValue.AsSpan(), true, out Big big);
    }

    [Benchmark, BenchmarkCategory("TryParseSpanIgnoreCase")]
    public bool TryParseSpanIgnoreCase_Extension()
    {
        return BigExtensions.TryParseIgnoreCase(BenchmarkValue.AsSpan(), out Big big);
    }
}
