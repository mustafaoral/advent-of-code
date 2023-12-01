namespace AdventOfCode.ExtensionMethods;

public static partial class IEnumerableOfTExtensions
{
    public static IEnumerable<(int Index, T Item)> SelectIndexed<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Select<T, (int, T)>((x, i) => new(i, x));
    }
}
