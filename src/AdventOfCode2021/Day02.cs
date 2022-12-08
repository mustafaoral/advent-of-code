namespace AdventOfCode2021;

public class Day02 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        var commandSumsByPlane = GetCommands(input)
            .GroupBy(x => x.Direction.Plane)
            .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(x => x.Value * x.Direction.ValueCoefficient).Sum());

        return commandSumsByPlane[Plane.Horizontal] * commandSumsByPlane[Plane.Vertical];
    }

    public int Part2(string input)
    {
        return GetCommands(input)
            .Aggregate(seed: new Accumulate(), (accumulate, command) =>
            {
                if (command.Direction.Plane == Plane.Vertical)
                {
                    accumulate.Pitch += command.Value * command.Direction.ValueCoefficient;
                }
                else
                {
                    accumulate.Vertical += command.Value * accumulate.Pitch;

                    accumulate.Horizontal += command.Value;
                }

                return accumulate;
            }, x => x.Horizontal * x.Vertical);
    }

    private static IEnumerable<Command> GetCommands(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var tokens = line.Split(" ");

                var direction = CommandDirection.Parse(tokens[0]);
                var value = int.Parse(tokens[1]);

                return new Command(direction, value);
            });
    }

    private class Accumulate
    {
        public int Pitch { get; set; }
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
    }

    private enum Plane
    {
        Horizontal,
        Vertical
    }

    private class CommandDirection
    {
        public static readonly CommandDirection Forward = new(Plane.Horizontal, 1);
        public static readonly CommandDirection Up = new(Plane.Vertical, -1);
        public static readonly CommandDirection Down = new(Plane.Vertical, 1);

        public Plane Plane { get; }
        public int ValueCoefficient { get; }

        public CommandDirection(Plane plane, int valueCoefficient)
        {
            Plane = plane;
            ValueCoefficient = valueCoefficient;
        }

        public static CommandDirection Parse(string s)
        {
            return s switch
            {
                "forward" => Forward,
                "up" => Up,
                "down" => Down,
                _ => null
            };
        }
    }

    private record Command(CommandDirection Direction, int Value);
}
