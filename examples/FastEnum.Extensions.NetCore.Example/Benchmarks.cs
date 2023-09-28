using System.Runtime.CompilerServices;

using BenchmarkDotNet.Attributes;

namespace ToStringExample;

[MemoryDiagnoser]
public class Benchmarks
{
    private Options options = Options.HasFlag;

    [Benchmark]
    public int UnsafeAs()
    {
        return Unsafe.As<Options, int>(ref options);
    }

    [Benchmark]
    public int Cast()
    {
        return ((int)options);
    }
}