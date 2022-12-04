namespace AdventOfCode2022;

public class Day04Alternative : IStringInputIntegerOutputChallenge
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

                return new AssignmentPair(new Assignment(int.Parse(tokens[0][0]), int.Parse(tokens[0][1])), new Assignment(int.Parse(tokens[1][0]), int.Parse(tokens[1][1])));
            })
            .ToArray();
    }

    private class AssignmentPair
    {
        public Assignment ElfA { get; set; }
        public Assignment ElfB { get; set; }

        private readonly int _length;

        public AssignmentPair(Assignment elfA, Assignment elfB)
        {
            ElfA = elfA;
            ElfB = elfB;

            _length = Math.Max(ElfA.EndSection, ElfB.EndSection);
        }

        public bool HasFullOverlap()
        {
            Span<int> a = stackalloc int[_length];
            Span<int> b = stackalloc int[_length];
            Span<int> result = stackalloc int[_length];

            Populate(a, ElfA);
            Populate(b, ElfB);
            BitwiseAnd(a, b, result);

            return result.SequenceEqual(a) || result.SequenceEqual(b);
        }

        public bool HasAnyOverlap()
        {
            Span<int> a = stackalloc int[_length];
            Span<int> b = stackalloc int[_length];
            Span<int> result = stackalloc int[_length];

            Populate(a, ElfA);
            Populate(b, ElfB);
            BitwiseAnd(a, b, result);

            return result.IndexOf(1) != -1;
        }

        private int[] CompareAsBitMap()
        {
            Span<int> a = stackalloc int[_length];
            Span<int> b = stackalloc int[_length];
            Span<int> result = stackalloc int[_length];

            Populate(a, ElfA);
            Populate(b, ElfB);
            BitwiseAnd(a, b, result);

            return result.ToArray();
        }

        private void BitwiseAnd(Span<int> a, Span<int> b, Span<int> result)
        {
            for (int i = 0; i < _length; i++)
            {
                result[i] = a[i] & b[i];
            }
        }

        private void Populate(Span<int> span, Assignment assignment)
        {
            for (int i = 0; i < _length; i++)
            {
                if (i >= assignment.StartSection - 1 && i < assignment.EndSection)
                {
                    span[i] = 1;
                }
            }
        }
    }

    private record Assignment(int StartSection, int EndSection);
}
