namespace AdventOfCode.ExtensionMethods;

public static class IEnumerableOfTExtensions
{
    public static IEnumerable<(int Index, T Item)> SelectIndexed<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Select<T, (int, T)>((x, i) => new(i, x));
    }

    public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this IEnumerable<T> enumerable, int size)
    {
        var length = enumerable.Count();
        var i = 0;

        while (i <= length - size)
        {
            yield return enumerable.Skip(i++).Take(size);
        }
    }
}
