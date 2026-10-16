namespace Lesson6PROCESS_Lab_;

internal static class Utils
{

    public static bool IsInt(string input) => int.TryParse(input, out _);

    public static void Stoper() { Console.Write("Press any key to continue..."); Console.ReadKey(); }


    public static int[] GetIntArr(int size)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = rand.Next(1, 100);
        }
        return arr;
    }

    public static void ShowArr(int[] arr)
    {
        Console.WriteLine($"\nArray contents:");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"{arr[i]} "); if ((i + 1) % 10 == 0) Console.WriteLine();
        }
    }
}