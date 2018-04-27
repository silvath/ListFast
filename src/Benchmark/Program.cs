using System;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ListFastAdd>();
            BenchmarkRunner.Run<ListFastContains>();
            Console.ReadKey();
        }
    }
}
