namespace AdventOfCode.ExtensionMethods;

public static partial class TwoDimensionalArrayExtensions
{
    public static T[,] SubArray<T>(this T[,] array, int rowStartIndex, int columnStartIndex, int height, int width)
    {
        var sub = new T[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                sub[i, j] = array[i + rowStartIndex, j + columnStartIndex];
            }
        }

        return sub;
    }

    public static T[] GetColumn<T>(this T[,] array, int columnIndex)
    {
        var height = array.GetLength(0);

        var column = new T[height];

        for (int i = 0; i < height; i++)
        {
            column[i] = array[i, columnIndex];
        }

        return column;
    }

    public static T[] GetRow<T>(this T[,] array, int rowIndex)
    {
        var width = array.GetLength(1);

        var row = new T[width];

        for (int i = 0; i < width; i++)
        {
            row[i] = array[rowIndex, i];
        }

        return row;
    }

    public static T[] GetDiagonal<T>(this T[,] array, bool increasingIndex)
    {
        var width = array.GetLength(1);

        var diagonal = new T[width];

        if (increasingIndex)
        {
            for (int i = 0; i < width; i++)
            {
                diagonal[i] = array[i, i];
            }
        }
        else
        {
            for (int i = width - 1; i >= 0; i--)
            {
                diagonal[i] = array[i, i];
            }
        }

        return diagonal;
    }

    public static IEnumerable<(int RowIndex, int ColumnIndex, T Item)> SelectIndexed<T>(this T[,] array)
    {
        var height = array.GetLength(0);
        var width = array.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                yield return new(i, j, array[i, j]);
            }
        }
    }
}
