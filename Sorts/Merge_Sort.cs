public static void MergeSort<T>(T[] array) where T : IComparable
{
    if (array.Length < 2) return;

    int middle = array.Length / 2;
    T[] left = new T[middle];
    T[] right = new T[array.Length - middle];

    Array.Copy(array, 0, left, 0, middle);
    Array.Copy(array, middle, right, 0, array.Length - middle);

    MergeSort(left);
    MergeSort(right);
    Merge(array, left, right);
}

private static void Merge<T>(T[] result, T[] left, T[] right) where T : IComparable
{
    int i = 0, j = 0, k = 0;
    while (i < left.Length && j < right.Length)
    {
        if (left[i].CompareTo(right[j]) <= 0)
        {
            result[k++] = left[i++];
        }
        else
        {
            result[k++] = right[j++];
        }
    }

    while (i < left.Length)
    {
        result[k++] = left[i++];
    }

    while (j < right.Length)
    {
        result[k++] = right[j++];
    }
}