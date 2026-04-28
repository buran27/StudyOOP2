using System;
using System.Diagnostics;
public static class PrimeCalculator {

    public static bool IsPrime(int number) {

        if (number < 2) return false;

        for (int i = 2; i * i <= number; i++) {
            if (number % i == 0) return false;            
        }

        return true;
    }

    public static void ResultOfCalculations(string version, int count, TimeSpan timeOfProccessing) {
        Console.WriteLine(version);
        Console.WriteLine($"Найдено {count} простых чисенл");
        Console.WriteLine($"Время работы {timeOfProccessing.TotalMilliseconds}");
    }

}