using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using thsilva;
using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes.Columns;

namespace Benchmark
{
    [MarkdownExporter, RankColumn]
    public class ListFastContains
    {
        [Params(100, 1_000)]
        public int Count { set; get; }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("List", "Contains")]
        public List<bool> Contains()
        {
            List<bool> contains = new List<bool>();
            List<int> list = new List<int>();
            for (int i = 0; i < Count; i++)
                list.Add(i);
            for (int i = 0; i < Count; i++)
                contains.Add(list.Contains(i * 2));
            return (contains);
        }

        [Benchmark]
        [BenchmarkCategory("SortedList", "Contains")]
        public List<bool> ContainsSorted()
        {
            List<bool> contains = new List<bool>();
            SortedList<int, int> list = new SortedList<int, int>();
            for (int i = 0; i < Count; i++)
                list.Add(i, i);
            for (int i = 0; i < Count; i++)
                contains.Add(list.ContainsKey(i * 2));
            return (contains);
        }

        [Benchmark]
        [BenchmarkCategory("ListFast", "Contains")]
        public List<bool> ContainsFast()
        {
            List<bool> contains = new List<bool>();
            ListFast<int> list = new ListFast<int>();
            for (int i = 0; i < Count; i++)
                list.Add(i);
            for (int i = 0; i < Count; i++)
                contains.Add(list.Contains(i * 2));
            return (contains);
        }
    }
}
