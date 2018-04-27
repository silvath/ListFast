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
    public class ListFastAdd
    {
        [Params(100, 1_000)]
        public int Count { set; get; }


        [Benchmark(Baseline = true)]
        [BenchmarkCategory("List", "Add")]
        public List<int> Add()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Count; i++)
                list.Add(i);
            return (list);
        }

        [Benchmark]
        [BenchmarkCategory("SortedList", "Add")]
        public SortedList<int,int> AddSorted()
        {
            SortedList<int, int> list = new SortedList<int, int>();
            for (int i = 0; i < Count; i++)
                list.Add(i,i);
            return (list);
        }

        [Benchmark]
        [BenchmarkCategory("ListFast", "Add")]
        public ListFast<int> AddFast()
        {
            ListFast<int> list = new ListFast<int>();
            for (int i = 0; i < Count; i++)
                list.Add(i);
            return (list);
        }
    }
}
