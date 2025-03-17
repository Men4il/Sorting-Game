public static void RadixSort(int[] array)
{
    int max = array.Max();

    for (int exp = 1; max / exp > 0; exp *= 10)
    {
        CountingSortByDigit(array, exp);
    }
}

private static void CountingSortByDigit(int[] array, int exp)
{
    int[] output = new int[array.Length];
    int[] count = new int[10];

    for (int i = 0; i < array.Length; i++)
    {
        count[(array[i] / exp) % 10]++;
    }

    for (int i = 1; i < 10; i++)
    {
        count[i] += count[i - 1];
    }

    for (int i = array.Length - 1; i >= 0; i--)
    {
        output[count[(array[i] / exp) % 10] - 1] = array[i];
        count[(array[i] / exp) % 10]--;
    }

    for (int i = 0; i < array.Length; i++)
    {
        array[i] = output[i];
    }
}