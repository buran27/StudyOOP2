using System;
using System.Threading;

public class MonitorVerison {
    private static int _counter = 0;
    private static readonly object _locker = new object();

    public void Run() {
        _counter = 0;
        Thread[] threads = new Thread[PrimeCalculator.ThreadCount];
        
        for (int i = 0; i < PrimeCalculator.ThreadCount; i++) {
            
        }

    }

    void Increment() {

        lock (_locker)
        {
        _counter++;
        }
}


}