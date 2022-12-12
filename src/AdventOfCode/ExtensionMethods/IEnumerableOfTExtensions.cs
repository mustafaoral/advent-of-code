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

    public static IEnumerable<(T First, T Second)> Pairwise<T>(this IEnumerable<T> enumerable)
    {
        var count = enumerable.Count();
        var i = 0;

        while (i <= count - 2)
        {
            var items = enumerable.Skip(i++).Take(2);

            yield return (items.ElementAt(0), items.ElementAt(1));
        }
    }
}
