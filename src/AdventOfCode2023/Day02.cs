namespace AdventOfCode2023;

public class Day02 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var games = GetGames(input);

        var round = new Round
        {
            Red = 12,
            Green = 13,
            Blue = 14,
        };

        return games.Where(game => game.Rounds.All(x => x.Red <= round.Red && x.Green <= round.Green && x.Blue <= round.Blue)).Sum(x => x.Id);
    }

    public int Part2(string input)
    {
        var games = GetGames(input);

        var maxReds = games.Select(game => game.Rounds.Max(x => x.Red));
        var maxGreens = games.Select(game => game.Rounds.Max(x => x.Green));
        var maxBlues = games.Select(game => game.Rounds.Max(x => x.Blue));

        return maxReds.Zip(maxGreens, maxBlues).Sum(x => x.First * x.Second * x.Third);
    }

    private static Game[] GetGames(string input)
    {
        return input.Split(Environment.NewLine)
            .Select(line =>
            {
                var game = new Game(int.Parse(Regex.Match(line, @"Game (?<id>\d+).+").Groups["id"].Value));
                var samples = line.Split(":")[1].Split(";");

                foreach (var sample in samples)
                {
                    var matches = sample.Split(",").Select(x => Regex.Match(x, @"(?<count>\d*) (?<colour>red|green|blue)"));

                    var round = matches.Select(x => new
                    {
                        Colour = Enum.Parse<Colour>(x.Groups["colour"].Value, true),
                        Count = int.Parse(x.Groups["count"].Value)
                    });

                    game.AddRound(
                        red: round.FirstOrDefault(x => x.Colour == Colour.Red)?.Count,
                        green: round.FirstOrDefault(x => x.Colour == Colour.Green)?.Count,
                        blue: round.FirstOrDefault(x => x.Colour == Colour.Blue)?.Count);
                }

                return game;
            }).ToArray();
    }

    private enum Colour
    {
        Red,
        Green,
        Blue
    }

    private class Round
    {
        public int Red { get; init; }
        public int Green { get; init; }
        public int Blue { get; init; }
    }

    private class Game(int id)
    {
        public int Id { get; } = id;
        public List<Round> Rounds { get; } = [];

        public void AddRound(int? red, int? green, int? blue)
        {
            Rounds.Add(new Round
            {
                Red = red ?? 0,
                Green = green ?? 0,
                Blue = blue ?? 0
            });
        }
    }
}
