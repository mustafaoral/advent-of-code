namespace AdventOfCode.ExtensionMethods;

public static partial class TwoDimensionalArrayExtensions
{
    public static void Fill<T>(this T[,] array, T value)
    {
        var height = array.GetLength(0);
        var width = array.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                array[i, j] = value;
            }
        }
    }

    public static void FillRow<T>(this T[,] array, int rowIndex, T value)
    {
        var width = array.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            array[rowIndex, i] = value;
        }
    }

    public static void FillColumn<T>(this T[,] array, int columnIndex, T value)
    {
        var height = array.GetLength(0);

        for (int i = 0; i < height; i++)
        {
            array[i, columnIndex] = value;
        }
    }
}
