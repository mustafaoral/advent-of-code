using System.Text;

namespace AdventOfCode.ExtensionMethods;

public static partial class TwoDimensionalArrayExtensions
{
    public static string AsMatrixText(this char[,] array)
    {
        var height = array.GetLength(0);
        var width = array.GetLength(1);

        var sb = new StringBuilder(height * width);

        for (int i = 0; i < height; i++)
        {
            if (i > 0)
            {
                sb.AppendLine();
            }

            for (int j = 0; j < width; j++)
            {
                sb.Append(array[i, j]);
            }
        }

        return sb.ToString();
    }

    public static string AsMatrixText<T>(this T[,] array, Predicate<T> predicate, Func<T, char> trueTransform, char falseValue)
    {
        return AsMatrixText(array, predicate, trueTransform, _ => falseValue);
    }

    public static string AsMatrixText<T>(this T[,] array, Predicate<T> predicate, char trueValue, Func<T, char> falseTransform)
    {
        return AsMatrixText(array, predicate, _ => trueValue, falseTransform);
    }

    public static string AsMatrixText<T>(this T[,] array, Predicate<T> predicate, char trueValue, char falseValue)
    {
        return AsMatrixText(array, predicate, _ => trueValue, _ => falseValue);
    }

    public static string AsMatrixText<T>(this T[,] array, Predicate<T> predicate, Func<T, char> trueTransform, Func<T, char> falseTransform)
    {
        var height = array.GetLength(0);
        var width = array.GetLength(1);

        var sb = new StringBuilder(height * width);

        for (int i = 0; i < height; i++)
        {
            if (i > 0)
            {
                sb.AppendLine();
            }

            for (int j = 0; j < width; j++)
            {
                var item = array[i, j];

                if (predicate(item))
                {
                    sb.Append(trueTransform(item));
                }
                else
                {
                    sb.Append(falseTransform(item));
                }
            }
        }

        return sb.ToString();
    }
}
