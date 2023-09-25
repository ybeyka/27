using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        int numberOfTasks = 1;

        if (args.Length > 0 && int.TryParse(args[0], out int input))
        {
            numberOfTasks = input;
        }

        Task<int>[] tasks = new Task<int>[numberOfTasks];

        Stopwatch stopwatch = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.Green; 
        for (int i = 0; i < numberOfTasks; i++)
        {
            tasks[i] = CalculateAsync(10);
            Console.WriteLine($"Task {i} started");
        }

        int[] results = await Task.WhenAll(tasks);
        stopwatch.Stop();

        for (int i = 0; i < numberOfTasks; i++)
        {
            Console.WriteLine($"Result of Task {i}: {results[i]}");
        }
        Console.WriteLine($"Total asynchronous  time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        Console.ForegroundColor = ConsoleColor.Red; 
        for (int i = 0; i < numberOfTasks; i++)
        {
            int result = Calculate(10);
            Console.WriteLine($"Result of synchronous calculation {i}: {result}");
        }
        stopwatch.Stop();
        Console.WriteLine($"Total synchronous  time: {stopwatch.ElapsedMilliseconds} ms");

        Console.ResetColor(); 
    }

    static async Task<int> CalculateAsync(int number)
    {
        await Task.Delay(100);
        return number * number;
    }

    static int Calculate(int number)
    {
        Task.Delay(100).Wait();
        return number * number;
    }
}
