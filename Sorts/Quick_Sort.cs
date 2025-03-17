public static void QuickSort<T>(T[] array, int left, int right) where T : IComparable
{
    if (left < right)
    {
        int pivotIndex = Partition(array, left, right);
        QuickSort(array, left, pivotIndex - 1);
        QuickSort(array, pivotIndex + 1, right);
    }
}

private static int Partition<T>(T[] array, int left, int right) where T : IComparable
{
    T pivot = array[right];
    int i = left - 1;
    for (int j = left; j < right; j++)
    {
        if (array[j].CompareTo(pivot) <= 0)
        {
            i++;
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
    T temp = array[i + 1];
    array[i + 1] = array[right];
    array[right] = temp;
    return i + 1;
}