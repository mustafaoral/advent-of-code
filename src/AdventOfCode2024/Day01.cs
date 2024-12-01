namespace AdventOfCode2024;

public class Day01 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var values = GetValues(input);

        var left = values.GetColumn(0).Order();
        var right = values.GetColumn(1).Order();

        return left.Zip(right, (x, y) => Math.Abs(x - y)).Sum();
    }

    public int Part2(string input)
    {
        var values = GetValues(input);

        var left = values.GetColumn(0);
        var right = values.GetColumn(1);

        return left.Select(l => right.Count(r => r == l) * l).Sum();
    }

    private static int[,] GetValues(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var values = new int[lines.Length, 2];

        for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
        {
            var matches = lines[rowIndex].MatchIntArray();

            values[rowIndex, 0] = matches[0];
            values[rowIndex, 1] = matches[1];
        }

        return values;
    }
}
