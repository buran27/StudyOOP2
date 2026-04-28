using System;
using System.Diagnostics;
using System.Threading;

namespace Study.Vers3;

class CounterWithSemaphore
{
    private readonly int _start; 
    private readonly int _end;
    private readonly int _threadCount;

    private int _primeCount;
    private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    public CounterWithSemaphore(int start, int end, int threadCount)
    {
        if(start >= end)
            throw new ArgumentException("Start can't be >= end");
        
        if(threadCount <= 0)
            throw new ArgumentException("Thread count can't be <= 0");

        _start = start;
        _end = end;
        _threadCount = threadCount;
    }

    public PrimeCounterResult Run()
    {
    _primeCount = 0;
    Thread[] threads = new Thread[_threadCount];
    int numbersCount = _end - _start + 1;
    int partSize = numbersCount / _threadCount;
    Stopwatch stopwatch = Stopwatch.StartNew();
    for (int i = 0; i < _threadCount; i++)
    {
        int threadNumber = i + 1;
          int rangeStart = _start + i * partSize;
        int rangeEnd = i == _threadCount - 1
            ? _end
            : rangeStart + partSize - 1;
        threads[i] = new Thread(() =>
        {
            CountPrimesInRange(threadNumber, rangeStart, rangeEnd);
        });
        threads[i].Start();
        }
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        stopwatch.Stop();
        return new PrimeCounterResult(_primeCount, stopwatch.ElapsedMilliseconds);
    }
    private void CountPrimesInRange(int threadNumber, int rangeStart, int rangeEnd)
    {
        for (int number = rangeStart; number <= rangeEnd; number++)
        {
            Console.WriteLine($"Поток {threadNumber}: обрабатывает число {number}");
            if (IsPrime(number))
            {
                _semaphore.Wait();
                try
                {
                    _primeCount++;
                    Console.WriteLine($"Поток {threadNumber}: найдено простое число {number}");
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
    }
    private static bool IsPrime(int number)
    {
        if (number < 2)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;
        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

}

public class PrimeCounterResult
{
    public int PrimeCount { get; }
    public long ElapsedMilliseconds { get; }

    public PrimeCounterResult(int primeCount, long elapsedMilliseconds)
    {
        PrimeCount = primeCount;
        ElapsedMilliseconds = elapsedMilliseconds;
    }
}