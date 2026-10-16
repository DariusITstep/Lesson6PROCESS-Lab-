using Lesson6PROCESS_Lab_;
using System;
using System.IO;
using System.Threading;
using static System.Console;

namespace T4;

internal static class Task4
{
    public static void Run()
    {
        Clear();

        WriteLine("===== MUTEX FILE WRITER =====");

        string path = "output.txt";

        FileWriter writer = new FileWriter(path);
        Thread[] threads = new Thread[4];

        for (int i = 0; i < threads.Length; i++)
        {
            int id = i + 1;

            threads[i] = new Thread(() =>
            {
                writer.Write($"Thread {id}: writing data.");
                WriteLine($"Thread {id} completed.");
            });

            threads[i].Start();
        }

        foreach (Thread thread in threads) thread.Join();

        WriteLine();
        WriteLine("===== FILE CONTENT =====");

        WriteLine(File.ReadAllText(path));

        Utils.Stoper();
    }
}

internal sealed class FileWriter
{
    private readonly string _path;
    private readonly Mutex _mutex = new Mutex();

    public FileWriter(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("File path cannot be empty.");

        _path = path;

        File.WriteAllText(_path, string.Empty);
    }

    public void Write(string text)
    {
        _mutex.WaitOne();

        try
        {
            File.AppendAllText(_path, text + Environment.NewLine);
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }
}