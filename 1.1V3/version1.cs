using System;
using System.Diagnostics;
using System.Threading;
using Study.Calculator;
using Study.Common;

namespace Study.Vers1;

public class CounterWithLock
{
    private readonly int _start; 
    private readonly int _end;
    private readonly int _threadCount;

    private int _primeCount;
    private readonly object _locker = new object();

    public CounterWithLock(int start, int end, int threadCount)
    {
        if (start >= end)
            throw new ArgumentException("Start can't be >= end");
        
        if (threadCount <= 0)
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
            int rangeEnd = i == _threadCount - 1 ? _end : rangeStart + partSize - 1;

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

            if (PrimeCalculator.IsPrime(number))
            {
                lock (_locker)
                {
                    _primeCount++;
                    Console.WriteLine($"Поток {threadNumber}: найдено простое число {number}");
                }
            }
        }
    }
}