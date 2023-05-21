using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WaysToIterate.Console;

[MemoryDiagnoser(false)]
public class LoopBenchmark
{
    private static readonly Random Rng = new(80085);

    [Params(100, 100_000, 1_000_000)] public int Size { get; set; }

    private List<int> _items;

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Size).Select(x => Rng.Next()).ToList();
    }

    /// <summary>
    /// Simple For Loop
    /// </summary>
    [Benchmark()]
    public void For()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            DoSomething(item);
        }
    }

    /// <summary>
    /// Simple Foreach Loop
    /// </summary>
    [Benchmark()]
    public void Foreach()
    {
        foreach (var item in _items)
        {
            DoSomething(item);
        }
    }

    /// <summary>
    /// Iteration with LINQ
    /// </summary>
    [Benchmark()]
    public void Foreach_Linq()
    {
        _items.ForEach(item => { DoSomething(item); });
    }

    /// <summary>
    /// Multithreading ForEach loop
    /// </summary>
    [Benchmark()]
    public void Parallel_ForEach()
    {
        Parallel.ForEach(_items, i => { DoSomething(i); });
    }

    /// <summary>
    /// Multithreading LINQ loop
    /// </summary>
    [Benchmark()]
    public void Parallel_Linq()
    {
        _items.AsParallel().ForAll(i => { DoSomething(i); });
    }

    /// <summary>
    /// Iteration with foreach but List as Span
    /// </summary>
    [Benchmark()]
    public void ForEach_Span()
    {
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {
            DoSomething(item);
        }
    }

    /// <summary>
    /// Iteration with for but list as span
    /// </summary>
    [Benchmark()]
    public void For_Span()
    {
        var asSpan = CollectionsMarshal.AsSpan(_items);
        for (var i = 0; i < asSpan.Length; i++)
        {
            var item = asSpan[i];
            DoSomething(item);
        }
    }
    
    /// <summary>
    /// For loop with Reference
    /// </summary>
    [Benchmark()]
    public void Unsafe_For_Span_GetReference()
    {
        Span<int> asSpan = _items.ToArray();
        ref var searchSpace = ref MemoryMarshal.GetReference(asSpan);
        for (var i = 0; i < asSpan.Length; i++)
        {
            var item = Unsafe.Add(ref searchSpace, i);
            DoSomething(item);
        }
    }

    /// <summary>
    /// Iteration with Unsafe Library
    /// </summary>
    [Benchmark()]
    public void Unsafe_Faster()
    {
        ref var start = ref MemoryMarshal.GetArrayDataReference(_items.ToArray());
        ref var end = ref Unsafe.Add(ref start, _items.Count);

        while (Unsafe.IsAddressLessThan(ref start, ref end))
        {
            DoSomething(start);
            start = ref Unsafe.Add(ref start, 1);
        }
    }
    
    /// <summary>
    /// Converts the List to Array and AsSpan
    /// </summary>
    [Benchmark()]
    public void Normal_For_AsSpan()
    {
        var span = _items.ToArray().AsSpan();
        for (var i = 0; i < span.Length; i++)
        {
            var item = _items[i];
            DoSomething(item);
        }
    }

    /// <summary>
    /// Loop with Range
    /// </summary>
    [Benchmark()]
    public void Weird_Loop_With_Range()
    {
        foreach (var i in 0.._items.Count)
        {
            DoSomething(i);
        }
    }
    
    /// <summary>
    /// Loop with Range
    /// </summary>
    [Benchmark()]
    public void Weird_Loop_With_Range_2()
    {
        foreach (var i in _items.Count)
        {
            DoSomething(i);
        }
    }

    private void DoSomething(int i)
    {
        
    }
}