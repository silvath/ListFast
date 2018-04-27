``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-6600U CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 2 physical cores
Frequency=2742187 Hz, Resolution=364.6724 ns, Timer=TSC
.NET Core SDK=2.1.104
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT


```
|         Method | Count |       Mean |      Error |     StdDev |     Median | Scaled | ScaledSD | Rank |
|--------------- |------ |-----------:|-----------:|-----------:|-----------:|-------:|---------:|-----:|
|       **Contains** |   **100** |  **11.966 us** |  **0.2287 us** |  **0.2139 us** |  **11.911 us** |   **1.00** |     **0.00** |    **3** |
| ContainsSorted |   100 |   9.124 us |  0.1791 us |  0.2942 us |   9.140 us |   0.76 |     0.03 |    2 |
|   ContainsFast |   100 |   7.327 us |  0.1431 us |  0.1704 us |   7.291 us |   0.61 |     0.02 |    1 |
|                |       |            |            |            |            |        |          |      |
|       **Contains** |  **1000** | **884.327 us** | **10.5718 us** |  **9.3716 us** | **884.014 us** |   **1.00** |     **0.00** |    **3** |
| ContainsSorted |  1000 | 163.165 us | 10.4213 us | 30.7275 us | 165.536 us |   0.18 |     0.03 |    2 |
|   ContainsFast |  1000 |  86.491 us |  3.8857 us | 11.0862 us |  81.292 us |   0.10 |     0.01 |    1 |
