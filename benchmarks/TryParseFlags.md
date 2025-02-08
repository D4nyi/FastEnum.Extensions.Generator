```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3037)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
```

|  Method                             | Categories                 | BenchmarkValue |            Mean |         Error |        StdDev |    Ratio |  RatioSD | Allocated | Alloc Ratio |
|-------------------------------------|----------------------------|----------------|----------------:|--------------:|--------------:|---------:|---------:|----------:|------------:|
| **TryParse_Original**               | **TryParse**               | **2048**       |    **15.46 ns** |  **0.168 ns** |  **0.157 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | 2048           |        12.86 ns |      0.071 ns |      0.059 ns |     0.83 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **65535**      |    **16.82 ns** |  **0.210 ns** |  **0.196 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | 65535          |        13.40 ns |      0.060 ns |      0.054 ns |     0.80 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Blue**       | **2,008.61 ns** | **19.234 ns** | **17.991 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Blue           |     1,758.08 ns |      8.490 ns |      7.942 ns |     0.88 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Green**      | **2,023.09 ns** | **22.968 ns** | **21.484 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Green          |     1,763.75 ns |      9.698 ns |      9.071 ns |     0.87 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Red**        | **2,020.65 ns** | **14.827 ns** | **13.869 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Red            |     1,785.33 ns |     10.901 ns |      9.103 ns |     0.88 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **2048**       |    **15.20 ns** |  **0.154 ns** |  **0.144 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | 2048           |        12.83 ns |      0.053 ns |      0.050 ns |     0.84 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **65535**      |    **16.51 ns** |  **0.197 ns** |  **0.184 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | 65535          |        13.44 ns |      0.106 ns |      0.094 ns |     0.81 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Blue**       | **1,263.43 ns** | **11.809 ns** | **10.468 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Blue           |     1,025.01 ns |     13.150 ns |     12.301 ns |     0.81 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Green**      | **1,277.63 ns** | **15.048 ns** | **14.076 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Green          |     1,032.60 ns |     14.417 ns |     13.486 ns |     0.81 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Red**        | **1,265.74 ns** |  **8.200 ns** |  **7.671 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Red            |     1,026.37 ns |     12.210 ns |     11.421 ns |     0.81 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **2048**       |    **12.17 ns** |  **0.112 ns** |  **0.105 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | 2048           |        12.76 ns |      0.255 ns |      0.238 ns |     1.05 |     0.02 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **65535**      |    **13.47 ns** |  **0.119 ns** |  **0.105 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | 65535          |        12.91 ns |      0.171 ns |      0.151 ns |     0.96 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Blue**       | **1,780.19 ns** | **19.826 ns** | **18.545 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Blue           |     1,771.16 ns |      9.721 ns |      9.093 ns |     1.00 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Green**      | **1,763.84 ns** | **16.128 ns** | **15.086 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Green          |     1,769.12 ns |     18.354 ns |     17.168 ns |     1.00 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Red**        | **1,791.57 ns** | **16.068 ns** | **15.030 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Red            |     1,759.57 ns |     15.702 ns |     14.688 ns |     0.98 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **2048**       |    **12.29 ns** |  **0.097 ns** |  **0.091 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | 2048           |        13.28 ns |      0.143 ns |      0.134 ns |     1.08 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **65535**      |    **13.61 ns** |  **0.248 ns** |  **0.232 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | 65535          |        13.38 ns |      0.174 ns |      0.162 ns |     0.98 |     0.02 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Blue**       | **1,266.88 ns** | **11.202 ns** | **10.478 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Blue           |     1,019.91 ns |     16.986 ns |     15.889 ns |     0.81 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Green**      | **1,277.42 ns** | **13.680 ns** | **12.796 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Green          |     1,018.03 ns |     11.846 ns |     10.501 ns |     0.80 |     0.01 |         - |          NA |
|                                     |                            |                |                 |               |               |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Red**        | **1,269.03 ns** |  **8.161 ns** |  **7.634 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Red            |     1,014.82 ns |      7.193 ns |      6.376 ns |     0.80 |     0.01 |         - |          NA |
