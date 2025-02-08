```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3037)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
```

| Method                 |  Categories   | BenchmarkValue |           Mean |         Error |        StdDev |         Median |     Ratio |  RatioSD |       Gen0 | Allocated | Alloc Ratio |
|------------------------|---------------|----------------|---------------:|--------------:|--------------:|---------------:|----------:|---------:|-----------:|----------:|------------:|
| **IsDefined_Original** | **IsDefined** | **Value0**     |  **1.7795 ns** | **0.0173 ns** | **0.0162 ns** |  **1.7790 ns** | **1.000** | **0.01** |      **-** |     **-** |      **NA** |
| IsDefined_Extension    | IsDefined     | Value0         |      0.0142 ns |     0.0074 ns |     0.0069 ns |      0.0132 ns |     0.008 |     0.00 |          - |         - |          NA |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **IsDefined_Original** | **IsDefined** | **Value512**   |  **1.7595 ns** | **0.0239 ns** | **0.0212 ns** |  **1.7532 ns** | **1.000** | **0.02** |      **-** |     **-** |      **NA** |
| IsDefined_Extension    | IsDefined     | Value512       |      0.0061 ns |     0.0080 ns |     0.0075 ns |      0.0001 ns |     0.003 |     0.00 |          - |         - |          NA |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **IsDefined_Original** | **IsDefined** | **Value1023**  |  **1.6896 ns** | **0.0316 ns** | **0.0296 ns** |  **1.6935 ns** |  **1.00** | **0.02** |      **-** |     **-** |      **NA** |
| IsDefined_Extension    | IsDefined     | Value1023      |      0.0262 ns |     0.0114 ns |     0.0106 ns |      0.0255 ns |      0.02 |     0.01 |          - |         - |          NA |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **IsDefined_Original** | **IsDefined** | **2048**       |  **1.7068 ns** | **0.0337 ns** | **0.0315 ns** |  **1.7094 ns** |  **1.00** | **0.03** |      **-** |     **-** |      **NA** |
| IsDefined_Extension    | IsDefined     | 2048           |      0.0204 ns |     0.0099 ns |     0.0093 ns |      0.0174 ns |      0.01 |     0.01 |          - |         - |          NA |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **ToString_Original**  | **ToString**  | **Value0**     | **10.2985 ns** | **0.1910 ns** | **0.1787 ns** | **10.3324 ns** |  **1.00** | **0.02** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Extension     | ToString      | Value0         |      0.4112 ns |     0.0302 ns |     0.0283 ns |      0.4132 ns |      0.04 |     0.00 |          - |         - |        0.00 |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **ToString_Original**  | **ToString**  | **Value512**   | **10.3635 ns** | **0.2587 ns** | **0.2420 ns** | **10.2780 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Extension     | ToString      | Value512       |      0.4778 ns |     0.0323 ns |     0.0302 ns |      0.4844 ns |      0.05 |     0.00 |          - |         - |        0.00 |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **ToString_Original**  | **ToString**  | **Value1023**  | **10.0766 ns** | **0.1322 ns** | **0.1172 ns** | **10.0573 ns** |  **1.00** | **0.02** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Extension     | ToString      | Value1023      |      0.4614 ns |     0.0223 ns |     0.0208 ns |      0.4586 ns |      0.05 |     0.00 |          - |         - |        0.00 |
|                        |               |                |                |               |               |                |           |          |            |           |             |
| **ToString_Original**  | **ToString**  | **2048**       | **19.0474 ns** | **0.3100 ns** | **0.2900 ns** | **19.0773 ns** |  **1.00** | **0.02** | **0.0090** |  **56 B** |    **1.00** |
| ToString_Extension     | ToString      | 2048           |      8.5775 ns |     0.1668 ns |     0.1560 ns |      8.5362 ns |      0.45 |     0.01 |     0.0051 |      32 B |        0.57 |
