public static void CountingSort(int[] array)
{
    if (array.Length == 0) return;

    int max = array.Max();
    int[] count = new int[max + 1];

    foreach (var num in array)
    {
        count[num]++;
    }

    int index = 0;
    for (int i = 0; i < count.Length; i++)
    {
        while (count[i] > 0)
        {
            array[index++] = i;
            count[i]--;
        }
    }
}