namespace AdventOfCode.ExtensionMethods;

public static partial class IEnumerableOfTExtensions
{
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

    public static void PairwiseExecute<TInput>(this IEnumerable<TInput> enumerable, Action<(TInput First, TInput Second)> pairwiseOperation)
    {
        var count = enumerable.Count();
        var i = 0;

        while (i <= count - 2)
        {
            var items = enumerable.Skip(i++).Take(2).ToArray();

            pairwiseOperation((items.ElementAt(0), items.ElementAt(1)));
        }
    }

    public static int PairwiseCount<TInput>(this IEnumerable<TInput> enumerable, Predicate<(TInput First, TInput Second)> predicate)
    {
        var enumerableCount = enumerable.Count();
        var i = 0;
        var count = 0;

        while (i <= enumerableCount - 2)
        {
            var items = enumerable.Skip(i++).Take(2).ToArray();

            if (predicate((items.ElementAt(0), items.ElementAt(1))))
            {
                count++;
            };
        }

        return count;
    }
}
