using Lesson6PROCESS_Lab_;
using System;
using System.Threading;
using static System.Console;

namespace T3;

internal static class Task3
{
    public static void Run()
    {
        Clear();

        WriteLine("===== TASK QUEUE =====");

        TaskQueue queue = new TaskQueue(3);

        for (int i = 1; i <= 10; i++) queue.AddTask(i);

        queue.StartAll();

        WriteLine();
        WriteLine($"Completed tasks: {queue.CompletedTasks}");

        Utils.Stoper();
    }
}

internal sealed class TaskQueue
{
    private readonly Semaphore _semaphore;
    private readonly Thread[] _threads;
    private int _completedTasks;

    public int CompletedTasks
    {
        get { return _completedTasks; }
    }

    public TaskQueue(int maxConcurrentTasks)
    {
        if (maxConcurrentTasks <= 0) throw new ArgumentException("Maximum concurrent tasks must be positive.");

        _semaphore = new Semaphore(maxConcurrentTasks, maxConcurrentTasks);

        _threads = new Thread[10];
    }

    public void AddTask(int taskId)
    {
        if (taskId < 1 || taskId > _threads.Length) throw new ArgumentException("Invalid task ID.");

        if (_threads[taskId - 1] != null) throw new ArgumentException("Task already exists.");

        _threads[taskId - 1] = new Thread(() => ProcessTask(taskId));
    }

    public void StartAll()
    {
        foreach (Thread thread in _threads)
        {
            if (thread == null) continue;
            thread.Start();
        }

        foreach (Thread thread in _threads)
        {
            if (thread == null)  continue;

            thread.Join();
        }
    }

    private void ProcessTask(int taskId)
    {
        _semaphore.WaitOne();

        try
        {
            WriteLine($"Task {taskId} started.");

            Thread.Sleep(1000);

            Interlocked.Increment(ref _completedTasks);

            WriteLine($"Task {taskId} completed.");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}