namespace Study.Common;

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