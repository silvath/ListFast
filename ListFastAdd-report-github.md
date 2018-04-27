``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.16299.371 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-6600U CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 2 physical cores
Frequency=2742187 Hz, Resolution=364.6724 ns, Timer=TSC
.NET Core SDK=2.1.104
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT


```
|    Method | Count |        Mean |       Error |    StdDev |      Median | Scaled | ScaledSD | Rank |
|---------- |------ |------------:|------------:|----------:|------------:|-------:|---------:|-----:|
|       **Add** |   **100** |    **532.7 ns** |    **11.22 ns** |  **29.55 ns** |    **523.0 ns** |   **1.00** |     **0.00** |    **1** |
| AddSorted |   100 |  5,556.3 ns |   122.82 ns | 250.88 ns |  5,517.4 ns |  10.46 |     0.72 |    3 |
|   AddFast |   100 |  3,261.8 ns |    64.33 ns |  98.24 ns |  3,237.0 ns |   6.14 |     0.37 |    2 |
|           |       |             |             |           |             |        |          |      |
|       **Add** |  **1000** |  **3,513.0 ns** |    **70.01 ns** | **176.92 ns** |  **3,485.7 ns** |   **1.00** |     **0.00** |    **1** |
| AddSorted |  1000 | 54,327.1 ns | 1,037.79 ns | 919.98 ns | 54,537.2 ns |  15.50 |     0.79 |    3 |
|   AddFast |  1000 | 20,154.3 ns |   399.24 ns | 667.03 ns | 20,023.0 ns |   5.75 |     0.34 |    2 |
