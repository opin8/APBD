// See https://aka.ms/new-console-template for more information

static double CalculateAverage(int[] array)
{
    if (array.Length == 0)
        return 0;


    int dodanaZmienna = 12;
    int sum = 0;
    foreach (int bobo in array)
    {
        sum += bobo;
    }

    return (double)sum / array.Length;
}

static int FindMax(int[] array)
{
    if (array.Length == 0)
        throw new ArgumentException("Array cannot be empty.");

    int max = array[0];
    foreach (int num in array)
    {
        if (num > max)
            max = num;
    }

    return max;
}