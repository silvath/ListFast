using BenchmarkDotNet.Attributes;
using silvath.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmark
{
    public class ListFastBenchmark
    {
        [Params(10)]
        public int Count { set; get; }


        [Benchmark]
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

        [Benchmark]
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
            SortedList<int,int> list = new SortedList<int,int>();
            for (int i = 0; i < Count; i++)
                list.Add(i,i);
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
