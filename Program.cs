using System;
using Study.Calculator; 
using Study.Vers1;      
using Study.Vers2;      
using Study.Vers3;      
using Study.Common;
using System.Diagnostics;
class Program
{
    static void Main()
    {
        const int start = 1;
        const int end = 10000;
        const int threadCount = 4;

        //lock
        CounterWithLock counter1 = new CounterWithLock(start, end, threadCount);
        PrimeCounterResult result1 = counter1.Run();
        
        PrimeCalculator.ResultOfCalculations(
            "Версия 1.1 (lock/Monitor)", 
            result1.PrimeCount, 
            TimeSpan.FromMilliseconds(result1.ElapsedMilliseconds)
        );

        //Mutex 

        CounterWithMutex counter2 = new CounterWithMutex(start, end, threadCount);
        PrimeCounterResult result2 = counter2.Run();
        
        PrimeCalculator.ResultOfCalculations(
            "Версия 1.2 (Mutex)", 
            result2.PrimeCount, 
            TimeSpan.FromMilliseconds(result2.ElapsedMilliseconds)
        );

        //SemaphoreSlim
        CounterWithSemaphore counter3 = new CounterWithSemaphore(start, end, threadCount);
        PrimeCounterResult result3 = counter3.Run();
        
        PrimeCalculator.ResultOfCalculations(
            "Версия 1.3 (SemaphoreSlim)", 
            result3.PrimeCount, 
            TimeSpan.FromMilliseconds(result3.ElapsedMilliseconds)
        );
    }

    // static async Task Main()
    // {
    //     string[] urls =
    //     {
    //         "https://jsonplaceholder.typicode.com/posts/1",
    //         "https://jsonplaceholder.typicode.com/users/1",
    //         // "https://jsonplaceholder.typicode.com/posts/999999" //запрос с ошибкой
    //     };

    //     AsyncJsonRequestService requestService = new AsyncJsonRequestService();

    //     Stopwatch stopwatch = Stopwatch.StartNew();

    //     try
    //     {
    //         List<RequestResult> results = await requestService.LoadAllAsync(urls);

    //         stopwatch.Stop();

    //         Console.WriteLine("=== JSON-ответы серверов ===");

    //         foreach (RequestResult result in results)
    //         {
    //             Console.WriteLine();
    //             Console.WriteLine($"Адрес: {result.Url}");
    //             Console.WriteLine(result.Json);
    //         }

    //         Console.WriteLine();
    //         Console.WriteLine($"Общее время выполнения: {stopwatch.ElapsedMilliseconds} мс");
    //     }
    //     catch (HttpRequestException ex)
    //     {
    //         stopwatch.Stop();

    //         Console.WriteLine("Ошибка при выполнении HTTP-запроса:");
    //         Console.WriteLine(ex.Message);
    //         Console.WriteLine($"Время до ошибки: {stopwatch.ElapsedMilliseconds} мс");
    //     }
    //     catch (Exception ex)
    //     {
    //         stopwatch.Stop();

    //         Console.WriteLine("Произошла ошибка:");
    //         Console.WriteLine(ex.Message);
    //         Console.WriteLine($"Время до ошибки: {stopwatch.ElapsedMilliseconds} мс");
    //     }
    // }
}
