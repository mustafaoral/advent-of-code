using System.Text.RegularExpressions;

namespace AdventOfCode.ExtensionMethods;

public static partial class StringExtensions
{
    public static int[] MatchIntArray(this string s) => NumberRegex().Matches(s).Select(x => int.Parse(x.Value)).ToArray();

    public static long[] MatchLongArray(this string s) => NumberRegex().Matches(s).Select(x => long.Parse(x.Value)).ToArray();

    [GeneratedRegex(@"\-?\d+(\.\d+)?")]
    private static partial Regex NumberRegex();
}
