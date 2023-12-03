namespace AdventOfCode2023;

public class Day03 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var lines = GetLines(input, symbolMatchPattern: @"[^\d.]");
        var numbers = new List<Number>();

        for (int i = 0; i < lines.Length; i++)
        {
            var (Upper, Middle, Lower) = GetLocalLines(lines, i);

            foreach (var number in Middle.Numbers)
            {
                if (Upper.Symbols.Any(symbol => IsSymbolAdjacent(number, symbol))
                    || Middle.Symbols.Any(symbol => IsSymbolAdjacent(number, symbol))
                    || Lower.Symbols.Any(symbol => IsSymbolAdjacent(number, symbol)))
                {
                    numbers.Add(number);
                }
            }
        }

        return numbers.Sum(x => x.Value);
    }

    public int Part2(string input)
    {
        var lines = GetLines(input, symbolMatchPattern: @"\*");
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var (Upper, Middle, Lower) = GetLocalLines(lines, i);

            foreach (var symbol in Middle.Symbols)
            {
                var matchingNumbersUpper = Upper.Numbers.Where(number => IsSymbolAdjacent(number, symbol)).ToArray();
                var matchingNumberMiddle = Middle.Numbers.Where(number => IsSymbolAdjacent(number, symbol)).ToArray();
                var matchingNumberLower = Lower.Numbers.Where(number => IsSymbolAdjacent(number, symbol)).ToArray();

                if (matchingNumbersUpper.Length + matchingNumberMiddle.Length + matchingNumberLower.Length == 2)
                {
                    sum += matchingNumbersUpper.Union(matchingNumberMiddle).Union(matchingNumberLower).Select(x => x.Value).Aggregate((accumulator, x) => accumulator * x);
                }
            }
        }

        return sum;
    }

    private static bool IsSymbolAdjacent(Number number, Symbol symbol) => symbol.Index >= number.Start - 1 && symbol.Index <= number.End + 1;

    private static (Line Upper, Line Middle, Line Lower) GetLocalLines(Line[] lines, int i)
    {
        var upper = lines[Math.Max(0, i - 1)];
        var middle = lines[i];
        var lower = lines[Math.Min(lines.Length - 1, i + 1)];

        return (upper, middle, lower);
    }

    private static Line[] GetLines(string input, string symbolMatchPattern)
    {
        return input.Split(Environment.NewLine)
            .Select(line =>
            {
                var numberMatches = Regex.Matches(line, @"\d+");
                var symbolMatches = Regex.Matches(line, symbolMatchPattern);

                return new Line
                {
                    Numbers = numberMatches.Select(x => new Number
                    {
                        Value = int.Parse(x.Value),
                        Start = x.Index,
                        End = x.Index + x.Length - 1
                    }).ToArray(),
                    Symbols = symbolMatches.Select(x => new Symbol
                    {
                        Index = x.Index
                    }).ToArray()
                };
            }).ToArray();
    }

    private class Line
    {
        public Number[] Numbers { get; init; }
        public Symbol[] Symbols { get; init; }
    }

    private class Number
    {
        public int Value { get; init; }
        public int Start { get; init; }
        public int End { get; init; }
    }

    private class Symbol
    {
        public int Index { get; init; }
    }
}
