using System;
using System.Diagnostics;
using Study.Vers3;


class Program
{
  // static void Main(){
  //       const int start = 1;
  //       const int end = 10000;
  //       const int threadCount = 4;

  //       CounterWithSemaphore counter = new CounterWithSemaphore(
  //           start,
  //           end,
  //           threadCount
  //       );

  //       PrimeCounterResult result = counter.Run();

  //       Console.WriteLine();
  //       Console.WriteLine("=== Результат работы программы ===");
  //       Console.WriteLine($"Общее количество простых чисел: {result.PrimeCount}");
  //       Console.WriteLine($"Время выполнения: {result.ElapsedMilliseconds} мс");
  // }
    static async Task Main()
    {
        string[] urls =
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/users/1",
            // "https://jsonplaceholder.typicode.com/posts/999999" //запрос с ошибкой
        };

        AsyncJsonRequestService requestService = new AsyncJsonRequestService();

        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            List<RequestResult> results = await requestService.LoadAllAsync(urls);

            stopwatch.Stop();

            Console.WriteLine("=== JSON-ответы серверов ===");

            foreach (RequestResult result in results)
            {
                Console.WriteLine();
                Console.WriteLine($"Адрес: {result.Url}");
                Console.WriteLine(result.Json);
            }

            Console.WriteLine();
            Console.WriteLine($"Общее время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }
        catch (HttpRequestException ex)
        {
            stopwatch.Stop();

            Console.WriteLine("Ошибка при выполнении HTTP-запроса:");
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Время до ошибки: {stopwatch.ElapsedMilliseconds} мс");
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Время до ошибки: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}