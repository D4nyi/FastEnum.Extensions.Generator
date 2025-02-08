```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3037)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
```

| Method                       |  Formats | BenchmarkValue |             Mean |          Error |         StdDev |     Ratio |  RatioSD |       Gen0 | Allocated | Alloc Ratio |
|------------------------------|----------|----------------|-----------------:|---------------:|---------------:|----------:|---------:|-----------:|----------:|------------:|
| **ToString_Format_Original** | **?**    | **Value0**     |    **11.394 ns** |  **0.2506 ns** |  **0.2345 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | ?        | Value0         |         1.899 ns |      0.0351 ns |      0.0328 ns |      0.17 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **?**    | **Value512**   |    **12.041 ns** |  **0.2939 ns** |  **0.2886 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | ?        | Value512       |         1.867 ns |      0.0465 ns |      0.0435 ns |      0.16 |     0.01 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **?**    | **Value1023**  |    **11.440 ns** |  **0.2170 ns** |  **0.2030 ns** |  **1.00** | **0.02** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | ?        | Value1023      |         1.872 ns |      0.0495 ns |      0.0463 ns |      0.16 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **?**    | **2048**       |    **20.158 ns** |  **0.3043 ns** |  **0.2846 ns** |  **1.00** | **0.02** | **0.0090** |  **56 B** |    **1.00** |
| ToString_Format_Extension    | ?        | 2048           |         9.942 ns |      0.2167 ns |      0.2027 ns |      0.49 |     0.01 |     0.0051 |      32 B |        0.57 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **d**    | **Value0**     |     **8.949 ns** |  **0.2150 ns** |  **0.2011 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | d        | Value0         |         2.727 ns |      0.0355 ns |      0.0332 ns |      0.30 |     0.01 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **d**    | **Value512**   |    **16.029 ns** |  **0.2440 ns** |  **0.2282 ns** |  **1.00** | **0.02** | **0.0089** |  **56 B** |    **1.00** |
| ToString_Format_Extension    | d        | Value512       |         9.785 ns |      0.1503 ns |      0.1255 ns |      0.61 |     0.01 |     0.0051 |      32 B |        0.57 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **d**    | **Value1023**  |    **16.290 ns** |  **0.2981 ns** |  **0.2788 ns** |  **1.00** | **0.02** | **0.0089** |  **56 B** |    **1.00** |
| ToString_Format_Extension    | d        | Value1023      |         9.454 ns |      0.1545 ns |      0.1446 ns |      0.58 |     0.01 |     0.0051 |      32 B |        0.57 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **d**    | **2048**       |    **16.128 ns** |  **0.3438 ns** |  **0.3216 ns** |  **1.00** | **0.03** | **0.0089** |  **56 B** |    **1.00** |
| ToString_Format_Extension    | d        | 2048           |         9.245 ns |      0.1235 ns |      0.1155 ns |      0.57 |     0.01 |     0.0051 |      32 B |        0.57 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **f**    | **Value0**     |    **15.206 ns** |  **0.3491 ns** |  **0.3266 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | f        | Value0         |         1.109 ns |      0.0474 ns |      0.0443 ns |      0.07 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **f**    | **Value512**   |   **272.094 ns** |  **1.6766 ns** |  **1.5682 ns** | **1.000** | **0.01** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | f        | Value512       |         1.082 ns |      0.0387 ns |      0.0362 ns |     0.004 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **f**    | **Value1023**  |    **15.403 ns** |  **0.3669 ns** |  **0.3604 ns** |  **1.00** | **0.03** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | f        | Value1023      |         1.113 ns |      0.0585 ns |      0.0547 ns |      0.07 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **f**    | **2048**       | **1,030.122 ns** | **10.8669 ns** | **10.1649 ns** |  **1.00** | **0.01** | **0.0076** |  **57 B** |    **1.00** |
| ToString_Format_Extension    | f        | 2048           |     1,025.362 ns |      8.6451 ns |      8.0866 ns |      1.00 |     0.01 |     0.0038 |      32 B |        0.56 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **g**    | **Value0**     |    **10.726 ns** |  **0.1525 ns** |  **0.1191 ns** |  **1.00** | **0.01** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | g        | Value0         |         2.520 ns |      0.0568 ns |      0.0503 ns |      0.23 |     0.01 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **g**    | **Value512**   |    **11.116 ns** |  **0.1906 ns** |  **0.1591 ns** |  **1.00** | **0.02** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | g        | Value512       |         2.521 ns |      0.0852 ns |      0.0755 ns |      0.23 |     0.01 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **g**    | **Value1023**  |    **10.719 ns** |  **0.1472 ns** |  **0.1377 ns** |  **1.00** | **0.02** | **0.0038** |  **24 B** |    **1.00** |
| ToString_Format_Extension    | g        | Value1023      |         2.477 ns |      0.0463 ns |      0.0433 ns |      0.23 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **g**    | **2048**       |    **19.673 ns** |  **0.2018 ns** |  **0.1888 ns** |  **1.00** | **0.01** | **0.0090** |  **56 B** |    **1.00** |
| ToString_Format_Extension    | g        | 2048           |        10.996 ns |      0.0784 ns |      0.0612 ns |      0.56 |     0.01 |     0.0051 |      32 B |        0.57 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **x**    | **Value0**     |    **18.805 ns** |  **0.3060 ns** |  **0.2862 ns** |  **1.00** | **0.02** | **0.0102** |  **64 B** |    **1.00** |
| ToString_Format_Extension    | x        | Value0         |         1.026 ns |      0.0390 ns |      0.0346 ns |      0.05 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **x**    | **Value512**   |    **18.666 ns** |  **0.2786 ns** |  **0.2606 ns** |  **1.00** | **0.02** | **0.0102** |  **64 B** |    **1.00** |
| ToString_Format_Extension    | x        | Value512       |         1.080 ns |      0.0481 ns |      0.0450 ns |      0.06 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **x**    | **Value1023**  |    **18.726 ns** |  **0.3006 ns** |  **0.2812 ns** |  **1.00** | **0.02** | **0.0102** |  **64 B** |    **1.00** |
| ToString_Format_Extension    | x        | Value1023      |         1.055 ns |      0.0583 ns |      0.0545 ns |      0.06 |     0.00 |          - |         - |        0.00 |
|                              |          |                |                  |                |                |           |          |            |           |             |
| **ToString_Format_Original** | **x**    | **2048**       |    **19.003 ns** |  **0.3643 ns** |  **0.3407 ns** |  **1.00** | **0.02** | **0.0102** |  **64 B** |    **1.00** |
| ToString_Format_Extension    | x        | 2048           |        11.086 ns |      0.0833 ns |      0.0779 ns |      0.58 |     0.01 |     0.0064 |      40 B |        0.62 |