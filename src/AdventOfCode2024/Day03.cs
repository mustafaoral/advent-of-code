namespace AdventOfCode2024;

public class Day03 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        return Regex.Matches(input, @"mul\((?<first>\d{1,3}),(?<second>\d{1,3})\)")
            .Select(x => int.Parse(x.Groups["first"].Value) * int.Parse(x.Groups["second"].Value))
            .Sum();
    }

    public int Part2(string input)
    {
        var multiplyInstructions = Regex.Matches(input, @"mul\((?<first>\d{1,3}),(?<second>\d{1,3})\)");
        var enableDisableInstructions = Regex.Matches(input, @"(do\(\)|don't\(\))");

        var index = 0;
        var enabled = true;
        var values = new List<int>();

        while (true)
        {
            if (index == input.Length)
            {
                break;
            }

            var multiplyInstruction = multiplyInstructions.FirstOrDefault(x => x.Index == index);
            var enableDisableInstruction = enableDisableInstructions.FirstOrDefault(x => x.Index == index);

            if (multiplyInstruction != null)
            {
                if (enabled)
                {
                    values.Add(int.Parse(multiplyInstruction.Groups["first"].Value) * int.Parse(multiplyInstruction.Groups["second"].Value));
                }

                index += multiplyInstruction.Length;
            }
            else if (enableDisableInstruction != null)
            {
                enabled = enableDisableInstruction.Value == "do()";

                index += enableDisableInstruction.Length;
            }
            else
            {
                index++;
            }
        }

        return values.Sum();
    }
}
