using System;
using Study.Vers3;


class Program
{
  static void Main(){
        const int start = 1;
        const int end = 10000;
        const int threadCount = 4;

        CounterWithSemaphore counter = new CounterWithSemaphore(
            start,
            end,
            threadCount
        );

        PrimeCounterResult result = counter.Run();

        Console.WriteLine();
        Console.WriteLine("=== Результат работы программы ===");
        Console.WriteLine($"Общее количество простых чисел: {result.PrimeCount}");
        Console.WriteLine($"Время выполнения: {result.ElapsedMilliseconds} мс");
  }
}