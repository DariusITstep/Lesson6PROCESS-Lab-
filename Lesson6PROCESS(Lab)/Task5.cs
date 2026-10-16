using Lesson6PROCESS_Lab_;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using static System.Console;

namespace T5;

internal static class Task5
{
    public static void Run()
    {
        Clear();

        WriteLine("===== ARRAY OPERATIONS =====");

        int[] numbers = Utils.GetIntArr(50);

        WriteLine("Initial array:");
        Utils.ShowArr(numbers);
        WriteLine();

        ArrayOperations operations = new ArrayOperations(numbers);

        Thread[] threads =
        {
            new Thread(() => operations.Add(10)),
            new Thread(() => operations.Multiply(2)),
            new Thread(() => operations.Add(5)),
            new Thread(() => operations.Multiply(3))
        };

        foreach (Thread thread in threads) thread.Start();
        foreach (Thread thread in threads) thread.Join();        

        WriteLine("\nFinal array:");      
        Utils.ShowArr(numbers);
        Utils.Stoper();
    }
}

internal sealed class ArrayOperations
{
    private readonly int[] _numbers;
    private readonly object _locker = new object();

    public ArrayOperations(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) throw new ArgumentException("Array cannot be empty.");

        _numbers = numbers;
    }

    public void Add(int value)
    {
        Monitor.Enter(_locker);

        try
        {
            WriteLine($"Add {value}: started.");

            for (int i = 0; i < _numbers.Length; i++) _numbers[i] += value;

            WriteLine($"Add {value}: completed.");
            Utils.ShowArr(_numbers);
            WriteLine();
        }
        finally
        {
            Monitor.Exit(_locker);
        }
    }

    public void Multiply(int value)
    {
        Monitor.Enter(_locker);

        try
        {
            WriteLine($"Multiply {value}: started.");

            for (int i = 0; i < _numbers.Length; i++) _numbers[i] *= value;

            WriteLine($"Multiply {value}: completed.");
            Utils.ShowArr(_numbers);
            WriteLine();
        }
        finally
        {
            Monitor.Exit(_locker);
        }
    }    
}