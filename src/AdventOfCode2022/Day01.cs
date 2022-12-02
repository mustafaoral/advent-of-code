namespace AdventOfCode2022;

public class Day01
{
    public static int Part1(string input)
    {
        return GetElves(input).MaxBy(x => x.TotalCalories).TotalCalories;
    }

    public static int Part2(string input)
    {
        return GetElves(input).OrderByDescending(x => x.TotalCalories).Take(3).Sum(x => x.TotalCalories);
    }

    private static List<Elf> GetElves(string input)
    {
        return input
            .Split(Environment.NewLine)
            .Aggregate(seed: new List<Elf> { new Elf() }, (elves, x) =>
            {
                var elf = elves.Last();

                if (int.TryParse(x, out var value))
                {
                    elf.AddCalories(value);

                    return elves;
                }

                elves.Add(new Elf());

                return elves;
            });
    }

    private class Elf
    {
        public int TotalCalories { get; private set; }

        public void AddCalories(int calories)
        {
            TotalCalories += calories;
        }
    }
}
