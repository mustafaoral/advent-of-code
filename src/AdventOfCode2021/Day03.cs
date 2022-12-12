namespace AdventOfCode2021;

public class Day03 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var readings = GetReadings(input);

        var gamma = ParseReadings(readings, reduceDigits: lookup => lookup.MaxBy(x => x.Count()));
        var epsilon = ParseReadings(readings, reduceDigits: lookup => lookup.MinBy(x => x.Count()));

        return gamma * epsilon;
    }

    private static int ParseReadings(string[] readings, Func<ILookup<string, string>, IGrouping<string, string>> reduceDigits)
    {
        var digits = readings.First().Length;

        return Enumerable.Range(0, digits).Aggregate(seed: new string[digits], (accumulate, digit) =>
        {
            accumulate[digit] = reduceDigits(readings.Select(x => x.Substring(digit, 1)).ToLookup(x => x)).Key;

            return accumulate;
        }, accumulate => Convert.ToInt32(accumulate.JoinWith(string.Empty), 2));
    }

    public int Part2(string input)
    {
        var readings = GetReadings(input);

        var oxygenGenerator = ParseReadings(readings, reduceDigits: lookup => lookup.MaxBy(x => x.Count()), relevantDigitOverride: "1");
        var co2Scrubber = ParseReadings(readings, reduceDigits: lookup => lookup.MinBy(x => x.Count()), relevantDigitOverride: "0");

        return oxygenGenerator * co2Scrubber;
    }

    private static int ParseReadings(string[] readings, Func<ILookup<string, string>, IGrouping<string, string>> reduceDigits, string relevantDigitOverride)
    {
        var digits = readings.First().Length;

        return Enumerable.Range(0, digits).Aggregate(seed: readings, (accumulate, digit) =>
        {
            if (accumulate.Length == 1)
            {
                return accumulate;
            }

            var readingsByDigit = accumulate.Select(x => x.Substring(digit, 1)).ToLookup(x => x);
            var relevantDigit = "0";

            if (readingsByDigit["0"].Count() == readingsByDigit["1"].Count())
            {
                relevantDigit = relevantDigitOverride;
            }
            else
            {
                relevantDigit = reduceDigits(accumulate.Select(x => x.Substring(digit, 1)).ToLookup(x => x)).Key;
            }

            return accumulate.Where(x => x.Substring(digit, 1) == relevantDigit).ToArray();
        }, x => Convert.ToInt32(x.Single(), 2));
    }

    private static string[] GetReadings(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
    }
}
