namespace AdventOfCode2022;

public class Day04 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        return GetAssignmentPairs(input)
            .Count(x => x.HasFullOverlap());
    }

    public int Part2(string input)
    {
        return GetAssignmentPairs(input)
            .Count(x => x.HasAnyOverlap());
    }

    private static AssignmentPair[] GetAssignmentPairs(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var tokens = line.Split(",").Select(x => x.Split("-")).ToArray();

                return new AssignmentPair
                {
                    ElfA = new Assignment(int.Parse(tokens[0][0]), int.Parse(tokens[0][1])),
                    ElfB = new Assignment(int.Parse(tokens[1][0]), int.Parse(tokens[1][1]))
                };
            })
            .ToArray();
    }

    private class AssignmentPair
    {
        public Assignment ElfA { get; set; }
        public Assignment ElfB { get; set; }

        public bool HasFullOverlap()
        {
            if (ElfA.StartSection <= ElfB.StartSection && ElfA.EndSection >= ElfB.EndSection)
            {
                return true;
            }

            if (ElfB.StartSection <= ElfA.StartSection && ElfB.EndSection >= ElfA.EndSection)
            {
                return true;
            }

            return false;
        }

        public bool HasAnyOverlap()
        {
            if (ElfA.EndSection >= ElfB.StartSection && ElfA.StartSection <= ElfB.EndSection)
            {
                return true;
            }

            if (ElfB.EndSection >= ElfA.StartSection && ElfB.StartSection <= ElfA.EndSection)
            {
                return true;
            }

            return false;
        }
    }

    private record Assignment(int StartSection, int EndSection);
}
