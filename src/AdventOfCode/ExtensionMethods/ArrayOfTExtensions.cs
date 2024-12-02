namespace AdventOfCode.ExtensionMethods;

public static class ArrayOfTExtensions
{
    public static IEnumerable<T[]> Masked<T>(this T[] array, int maskingWidth)
    {
        var count = array.Length - maskingWidth + 1;

        for (int i = 0; i < count; i++)
        {
            yield return array[0..i].Concat(array[(i + maskingWidth)..]).ToArray();
        }
    }
}
