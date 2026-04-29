

public class NumberSetProcessor
{
  private readonly List<int[]> _numSets;
  private readonly int _maxWorkingThreads;

  public NumberSetProcessor(List<int[]> numSets, int maxThreads)
  {
    if(numSets == null)
      throw new ArgumentException("Num Sets cant be empty(null)");
    if (maxThreads <= 0)
      throw new ArgumentException("Max working threads count cant be <= 0");

    _numSets = numSets;
    _maxWorkingThreads = maxThreads;
  }

    public void Process()
    {
        Thread[] threads = new Thread[_numSets.Count];

        using SemaphoreSlim semaphore = new SemaphoreSlim(
            _maxWorkingThreads,
            _maxWorkingThreads
        );

        for (int i = 0; i < _numSets.Count; i++)
        {
            int setIndex = i;
            int setNumber = i + 1;

            threads[i] = new Thread(() =>
            {
                ProcessSingleSet(setNumber, _numSets[setIndex], semaphore);
            });

            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }

    private void ProcessSingleSet(
        int setNumber,
        int[] numbers,
        SemaphoreSlim semaphore)
    {
        semaphore.Wait();

        try
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine($"Поток {threadId} начал обработку набора {setNumber}");

            int sum = numbers.Sum();

          //тут нужно делать добовление

            Console.WriteLine($"Поток {threadId} закончил обработку набора {setNumber}");
        }
        finally
        {
            semaphore.Release();
        }
    }

}