using Lesson6PROCESS_Lab_;
using System.Threading;
using static System.Console;

namespace T2;

internal static class Task2
{
    public static void Run()
    {
        Clear();

        WriteLine("===== COMPLETED TASKS =====");

        int completedTasks = 0;
        Thread[] threads = new Thread[4];

        for (int i = 0; i < threads.Length; i++)
        {
            int id = i + 1;

            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 100; j++)
                {                   
                    Interlocked.Increment(ref completedTasks);
                }

                WriteLine($"Thread {id} completed.");
            });

            threads[i].Start();
        }

        foreach (Thread thread in threads) thread.Join();

        WriteLine();
        WriteLine($"Completed tasks: {completedTasks}");

        Utils.Stoper();
    }
}