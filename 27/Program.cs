using System;
using System.Diagnostics;
using System.Threading.Tasks;
//using System.Threading;
class Program
{
    static async Task Main(string[] args)
    {
        int numberOfTasks = 1;

        if (args.Length > 0)
        {
            if (int.TryParse(args[0], out int input) && input > 0)
            {
                numberOfTasks = input;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid input. Number of tasks should be a positive integer. Using default value: 1.");
                Console.ResetColor();
            }
        }

        Task<int>[] tasks = new Task<int>[numberOfTasks];

        Stopwatch stopwatch = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < numberOfTasks; i++)
        {
            try
            {
                tasks[i] = CalculateAsync(10);
                Console.WriteLine($"Task {i} started");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in asynchronous task {i}: {ex.Message}");
            }
        }

        try
        {
            int[] results = await Task.WhenAll(tasks);
            stopwatch.Stop();
            for (int i = 0; i < numberOfTasks; i++)
            {
                Console.WriteLine($"Result of Task {i}: {results[i]}");
            }
            Console.WriteLine($"Total asynchronous execution time: {stopwatch.ElapsedMilliseconds} ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while awaiting tasks: {ex.Message}");
        }

        stopwatch.Restart();
        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < numberOfTasks; i++)
        {
            try
            {
                int result = Calculate(10);
                Console.WriteLine($"Result of synchronous calculation {i}: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in synchronous calculation {i}: {ex.Message}");
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Total synchronous execution time: {stopwatch.ElapsedMilliseconds} ms");

        Console.ResetColor();
    }

    static async Task<int> CalculateAsync(int number)
    {
        
        await Task.Delay(100);
        return number * number;
    }

    static int Calculate(int number)
    {
        //Thread.Sleep(100);
         Task.Delay(100).Wait();
        return number * number;
    }
}
