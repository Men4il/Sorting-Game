public static void InsertionSort<T>(T[] array) where T : IComparable
{
    int n = array.Length;
    for (int i = 1; i < n; i++)
    {
        T key = array[i];
        int j = i - 1;
        while (j >= 0 && array[j].CompareTo(key) > 0)
        {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = key;
    }
}