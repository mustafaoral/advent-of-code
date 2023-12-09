namespace AdventOfCode2023;

public class Day06 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var races = lines[0].MatchIntArray()
            .Zip(lines[1].MatchIntArray())
            .Select(x => new Race(x.First, x.Second)).ToArray();

        return GetWinningWays(races);
    }

    public int Part2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var races = lines[0].Split(" ").JoinWith(string.Empty).MatchLongArray()
            .Zip(lines[1].Split(" ").JoinWith(string.Empty).MatchLongArray())
            .Select(x => new Race(x.First, x.Second)).ToArray();

        return GetWinningWays(races);
    }

    private static int GetWinningWays(Race[] races)
    {
        return races.Aggregate(1, (accumulate, race) =>
        {
            var count = 0;

            for (int i = 0; i < race.Time; i++)
            {
                if (i * (race.Time - i) > race.Distance)
                {
                    count++;
                }
            }

            return accumulate *= count;
        });
    }

    private record Race(long Time, long Distance);
}
