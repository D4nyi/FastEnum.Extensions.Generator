```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3037)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX2
```

|  Method                             | Categories                 | BenchmarkValue |              Mean |          Error |         StdDev |    Ratio |  RatioSD | Allocated | Alloc Ratio |
|-------------------------------------|----------------------------|----------------|------------------:|---------------:|---------------:|---------:|---------:|----------:|------------:|
| **TryParse_Original**               | **TryParse**               | **?**          |     **0.2369 ns** |  **0.0130 ns** |  **0.0115 ns** | **1.00** | **0.07** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | ?              |         0.6810 ns |      0.0452 ns |      0.0484 ns |     2.88 |     0.24 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **2048**       |    **15.3601 ns** |  **0.1600 ns** |  **0.1419 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | 2048           |        12.8518 ns |      0.0977 ns |      0.0816 ns |     0.84 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Value0**     |    **15.9654 ns** |  **0.1891 ns** |  **0.1769 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Value0         |        13.1235 ns |      0.1544 ns |      0.1444 ns |     0.82 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Value1023**  | **1,822.4086 ns** | **33.6388 ns** | **28.0900 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Value1023      |     1,591.0927 ns |     18.6163 ns |     16.5029 ns |     0.87 |     0.02 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParse_Original**               | **TryParse**               | **Value512**   | **1,556.8531 ns** |  **8.8268 ns** |  **7.8248 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParse_Extension                  | TryParse                   | Value512       |     1,536.9023 ns |     16.8905 ns |     15.7994 ns |     0.99 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **?**          |     **0.0268 ns** |  **0.0080 ns** |  **0.0067 ns** | **1.10** | **0.54** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | ?              |         0.6478 ns |      0.0212 ns |      0.0198 ns |    26.62 |    11.04 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **2048**       |    **15.5139 ns** |  **0.2198 ns** |  **0.2056 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | 2048           |        13.0845 ns |      0.2247 ns |      0.2102 ns |     0.84 |     0.02 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Value0**     |    **18.6537 ns** |  **0.2387 ns** |  **0.2233 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Value0         |        15.6501 ns |      0.1221 ns |      0.1142 ns |     0.84 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Value1023**  | **1,374.5573 ns** |  **4.6251 ns** |  **3.8621 ns** | **1.00** | **0.00** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Value1023      |     1,150.2127 ns |     13.7808 ns |     12.8906 ns |     0.84 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseIgnoreCase_Original**     | **TryParseIgnoreCase**     | **Value512**   | **2,510.4184 ns** | **23.9589 ns** | **22.4112 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseIgnoreCase_Extension        | TryParseIgnoreCase         | Value512       |     2,221.3837 ns |     20.3968 ns |     18.0812 ns |     0.88 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **?**          |     **2.5262 ns** |  **0.0498 ns** |  **0.0466 ns** | **1.00** | **0.03** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | ?              |         0.4163 ns |      0.0251 ns |      0.0235 ns |     0.16 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **2048**       |    **12.1581 ns** |  **0.0690 ns** |  **0.0646 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | 2048           |        12.4807 ns |      0.1365 ns |      0.1276 ns |     1.03 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Value0**     |    **17.7419 ns** |  **0.1685 ns** |  **0.1576 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Value0         |        12.8958 ns |      0.2350 ns |      0.2199 ns |     0.73 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Value1023**  | **1,857.5029 ns** | **23.5440 ns** | **22.0231 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Value1023      |     1,573.7532 ns |     11.0704 ns |     10.3553 ns |     0.85 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpan_Original**           | **TryParseSpan**           | **Value512**   | **1,560.8353 ns** | **30.4518 ns** | **33.8471 ns** | **1.00** | **0.03** |     **-** |      **NA** |
| TryParseSpan_Extension              | TryParseSpan               | Value512       |     1,385.7726 ns |     11.2401 ns |      9.9641 ns |     0.89 |     0.02 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **?**          |     **2.4497 ns** |  **0.0293 ns** |  **0.0274 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | ?              |         0.6729 ns |      0.0339 ns |      0.0301 ns |     0.27 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **2048**       |    **12.2033 ns** |  **0.0839 ns** |  **0.0785 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | 2048           |        12.9808 ns |      0.0989 ns |      0.0926 ns |     1.06 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Value0**     |    **19.8786 ns** |  **0.1414 ns** |  **0.1253 ns** | **1.00** | **0.01** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Value0         |        15.7194 ns |      0.1336 ns |      0.1250 ns |     0.79 |     0.01 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Value1023**  | **1,375.9447 ns** | **18.3933 ns** | **17.2051 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Value1023      |     1,155.4432 ns |     22.3699 ns |     20.9249 ns |     0.84 |     0.02 |         - |          NA |
|                                     |                            |                |                   |                |                |          |          |           |             |
| **TryParseSpanIgnoreCase_Original** | **TryParseSpanIgnoreCase** | **Value512**   | **2,225.4552 ns** | **31.9215 ns** | **26.6559 ns** | **1.00** | **0.02** |     **-** |      **NA** |
| TryParseSpanIgnoreCase_Extension    | TryParseSpanIgnoreCase     | Value512       |     2,312.0035 ns |     20.9872 ns |     19.6315 ns |     1.04 |     0.01 |         - |          NA |