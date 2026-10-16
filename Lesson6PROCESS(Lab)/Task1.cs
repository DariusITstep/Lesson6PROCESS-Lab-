using Lesson6PROCESS_Lab_;
using static System.Console;

namespace T1;

internal static class Task1
{
    public static void Run()
    {
        Clear();

        WriteLine("===== BANK ACCOUNT =====");

        BankAccount account = new BankAccount(1000m);

        Thread[] threads = new Thread[4];

        try
        {
            for (int i = 0; i < threads.Length; i++)
            {
                int id = i + 1;

                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        account.Deposit(100m);
                        account.Withdraw(50m);
                    }

                    WriteLine($"Thread {id} completed.");
                });

                threads[i].Start();
            }
        }
        catch (Exception ex)
        {
            WriteLine($"An error occurred: {ex.Message}");
        }

        foreach (Thread thread in threads) thread.Join();

        WriteLine();
        WriteLine($"Initial balance: 1000.00");
        WriteLine($"Final balance: {account.Balance:F2}");

        Utils.Stoper();
    }
}

internal sealed class BankAccount
{
    private readonly object _locker = new object();
    private decimal _balance;

    public decimal Balance
    {
        get { lock (_locker) return _balance; }
    }

    public BankAccount(decimal initialBalance)
    {
        if (initialBalance < 0) throw new ArgumentException("Initial balance cannot be negative.");

        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");

        lock (_locker)
        {
            _balance += amount;
        }
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");

        lock (_locker)
        {
            if (amount > _balance) return false;
            _balance -= amount;
            return true;
        }
    }
}