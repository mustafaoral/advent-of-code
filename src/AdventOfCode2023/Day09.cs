namespace AdventOfCode2023;

public class Day09 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        return Solve(input, lineNumberSelector: x => x[^1], reduce: x => x.Sum());
    }

    public int Part2(string input)
    {
        return Solve(input, lineNumberSelector: x => x[0], reduce: x => x.AsEnumerable().Reverse().Aggregate((x, y) => y - x));
    }

    private static int Solve(string input, Func<int[], int> lineNumberSelector, Func<List<int>, int> reduce)
    {
        return input.Split(Environment.NewLine)
            .Select(line =>
            {
                var lineNumbers = line.MatchIntArray();
                var reduceInput = new List<int>();

                while (true)
                {
                    reduceInput.Add(lineNumberSelector(lineNumbers));

                    lineNumbers = lineNumbers.Pairwise(x => x.Second - x.First).ToArray();

                    if (lineNumbers.All(x => x == 0))
                    {
                        break;
                    }
                }

                return reduce(reduceInput);
            })
            .Sum();
    }
}
