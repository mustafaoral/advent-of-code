namespace AdventOfCode.ExtensionMethods;

public static partial class IEnumerableOfTExtensions
{
    public static IEnumerable<IEnumerable<T>> SlidingWindow<T>(this IEnumerable<T> enumerable, int size)
    {
        var length = enumerable.Count();
        var i = 0;

        while (i <= length - size)
        {
            yield return enumerable.Skip(i++).Take(size);
        }
    }

    public static IEnumerable<T> SlidingWindow<T>(this IEnumerable<T> enumerable, int size, Func<IEnumerable<T>, T> windowReduce)
    {
        var length = enumerable.Count();
        var i = 0;

        while (i <= length - size)
        {
            yield return windowReduce(enumerable.Skip(i++).Take(size));
        }
    }
}
