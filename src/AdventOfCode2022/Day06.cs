namespace AdventOfCode2022;

public class Day06 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        return GetIndex(input.Trim(), packetLength: 4);
    }

    public int Part2(string input)
    {
        return GetIndex(input.Trim(), packetLength: 14);
    }

    private static int GetIndex(string input, int packetLength)
    {
        for (int i = 0; i <= input.Length - packetLength; i++)
        {
            if (input.Skip(i).Take(packetLength).Distinct().Count() == packetLength)
            {
                return i + packetLength;
            }
        }

        return -1;
    }
}
