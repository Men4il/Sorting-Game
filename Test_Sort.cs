using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int[] array = GenerateRandomArray(100, -1000, 1000);

        Console.WriteLine("Начальный массив:");
        PrintArray(array);

        Stopwatch stopwatch = new Stopwatch();
        int iterationCount = 0;

        stopwatch.Start();

        BubbleSort(array, ref iterationCount);

        stopwatch.Stop();

        Console.WriteLine("\nОтсортированный массив:");
        PrintArray(array);

        Console.WriteLine($"\nКоличество итераций: {iterationCount}");
        Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalSeconds:F6} сек.");
    }

    static int[] GenerateRandomArray(int size, int minValue, int maxValue)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(minValue, maxValue + 1);
        }
        return array;
    }

    static void PrintArray(int[] array)
    {
        Console.WriteLine(string.Join(", ", array));
    }

    static void BubbleSort(int[] array, ref int iterationCount)
    {
        int n = array.Length;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < n - i - 1; j++)
            {
                iterationCount++;
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                    swapped = true;
                }
            }

            if (!swapped) break;
        }
    }
}