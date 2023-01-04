namespace AdventOfCode2022;

public class Day13 : IStringInputPuzzle<int, int>
{
    public int Part1(string input)
    {
        return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Chunk(2)
            .SelectIndexed()
            .Where(x => PacketComparer.Instance.Compare(x.Item[0], x.Item[1]) < 0)
            .Select(x => x.Index + 1)
            .Sum();
    }

    public int Part2(string input)
    {
        var orderedItems = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .ToList()
            .Append("[[2]]")
            .Append("[[6]]")
            .OrderBy(x => x, PacketComparer.Instance)
            .SelectIndexed()
            .ToArray();

        return (orderedItems.First(x => x.Item == "[[2]]").Index + 1) * (orderedItems.First(x => x.Item == "[[6]]").Index + 1);
    }

    private class PacketComparer : Comparer<string>
    {
        public static readonly Comparer<string> Instance = new PacketComparer();

        public override int Compare(string left, string right)
        {
            for (int i = 0; i < Math.Min(left.Length, right.Length); i++)
            {
                var leftIsDigit = char.IsDigit(left[i]);
                var rightIsDigit = char.IsDigit(right[i]);

                if (leftIsDigit && !rightIsDigit)
                {
                    return Compare($"[{GetNextNumber(left, i)}]", right[i..]);
                }

                if (rightIsDigit && !leftIsDigit)
                {
                    return Compare(left[i..], $"[{GetNextNumber(right, i)}]");
                }

                if (leftIsDigit && rightIsDigit)
                {
                    var leftValue = GetNextNumber(left, i);
                    var rightValue = GetNextNumber(right, i);

                    if (leftValue == rightValue)
                    {
                        continue;
                    }

                    return leftValue.CompareTo(rightValue);
                }

                var comparison = left[i].CompareTo(right[i]);

                if (comparison == 0)
                {
                    continue;
                }

                if (comparison > 0)
                {
                    return -1;
                }

                return 1;
            }

            return 1;
        }

        private static int GetNextNumber(ReadOnlySpan<char> chars, int index)
        {
            var rest = chars[index..];
            var indexOfNonNumber = rest.IndexOfAny(',', ']');

            return int.Parse(rest[..indexOfNonNumber]);
        }
    }
}
