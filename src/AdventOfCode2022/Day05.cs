using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day05 : IStringInputStringOutputChallenge
{
    public string Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var stacks = GetStacks(lines);
        var moves = GetMoves(lines);

        foreach (var move in moves)
        {
            for (int i = 0; i < move.Count; i++)
            {
                stacks[move.ToIndex].Push(stacks[move.FromIndex].Pop());
            }
        }

        return stacks.Select(x => x.Pop()).JoinWith("");
    }

    public string Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var stacks = GetStacks(lines);
        var moves = GetMoves(lines);

        foreach (var move in moves)
        {
            var items = new List<string>();

            for (int i = 0; i < move.Count; i++)
            {
                var item = stacks[move.FromIndex].Pop();

                items.Add(item);
            }

            items.Reverse();

            for (int i = 0; i < items.Count; i++)
            {
                stacks[move.ToIndex].Push(items[i]);
            }
        }

        return stacks.Select(x => x.Pop()).JoinWith("");
    }

    private static Stack<string>[] GetStacks(IEnumerable<string> lines)
    {
        var levels = lines
            .TakeWhile(x => x.Trim().StartsWith("["))
            .Reverse()
            .ToArray();

        var stackCount = (levels[0].Length + 1) / 4;
        var stacks = Enumerable.Range(0, stackCount).Select(_ => new Stack<string>()).ToArray();

        foreach (var level in levels)
        {
            var levelItems = (" " + level).SplitToTokensInLength(4)
                .Select(x => Regex.Match(x, @"\w").Value)
                .ToArray();

            for (int i = 0; i < levelItems.Length; i++)
            {
                var item = levelItems[i];

                if (!string.IsNullOrWhiteSpace(item))
                {
                    stacks[i].Push(item);
                }
            }
        }

        return stacks;
    }

    private static Move[] GetMoves(IEnumerable<string> lines)
    {
        return lines
            .SkipWhile(x => !x.StartsWith("move"))
            .Select((line, i) =>
            {
                var match = Regex.Match(line, @"move (?<count>\d+) from (?<fromPosition>\d+) to (?<toPosition>\d+)");

                var count = int.Parse(match.Groups["count"].Value);
                var fromIndex = int.Parse(match.Groups["fromPosition"].Value) - 1;
                var toIndex = int.Parse(match.Groups["toPosition"].Value) - 1;

                return new Move(i + 1, count, fromIndex, toIndex);
            })
            .ToArray();
    }

    private record Move(int Number, int Count, int FromIndex, int ToIndex);
}
