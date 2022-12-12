using System.Text;

namespace AdventOfCode2022;

public class Day10 : IStringInputPuzzle<int, string>
{
    public int Part1(string input)
    {
        var instructions = GetInstructions(input);

        return Enumerable.Range(0, 6)
            .Select(x => 20 + x * 40)
            .Sum(cycle => (instructions.Where(x => x.StartCycle + x.Duration < cycle).Sum(x => x.Change) + 1) * cycle);
    }

    public string Part2(string input)
    {
        var instructions = GetInstructions(input);

        return Enumerable.Range(0, 240)
            .Aggregate(seed: new StringBuilder(240), (accumulate, cycle) =>
            {
                if (cycle > 0 && cycle % 40 == 0)
                {
                    accumulate.AppendLine();
                }

                var spriteStartIndexAtCycle = instructions.Where(x => x.StartCycle + x.Duration <= cycle).Sum(x => x.Change);

                var pixelValue = cycle % 40 >= spriteStartIndexAtCycle && cycle % 40 <= spriteStartIndexAtCycle + 2 ? '#' : '.';

                return accumulate.Append(pixelValue);
            }, x => x.ToString());
    }

    private static Instruction[] GetInstructions(string input)
    {
        var instructions = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(x =>
            {
                if (x == "noop")
                {
                    return new Instruction();
                }

                return new Instruction { Duration = 2, Change = int.Parse(x[4..]) };
            })
            .ToArray();

        instructions.Pairwise(x => x.Second.StartCycle = x.First.StartCycle + x.First.Duration);

        return instructions;
    }

    private class Instruction
    {
        public int StartCycle { get; set; }
        public int Duration { get; set; } = 1;
        public int Change { get; set; }
    }
}
