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

    public static void Pairwise<T>(this IEnumerable<T> enumerable, Action<(T First, T Second)> pairwiseOperation)
    {
        foreach (var item in enumerable.Pairwise())
        {
            pairwiseOperation(item);
        }
    }

    public static IEnumerable<T> Pairwise<T>(this IEnumerable<T> enumerable, Func<(T First, T Second), T> pairwiseOperation) => enumerable.Pairwise().Select(pairwiseOperation);

    public static int PairwiseCount<T>(this IEnumerable<T> enumerable, Func<(T First, T Second), bool> predicate) => enumerable.Pairwise().Count(predicate);
}
