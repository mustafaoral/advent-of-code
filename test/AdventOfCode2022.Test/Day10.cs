namespace AdventOfCode2022.Test;

public class Day10 : SharedStringInputTest<int, string>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(12640, output);
    }

    protected override void Part2Assert(string output)
    {
        var expectedOutput = @"
####.#..#.###..####.#....###....##.###..
#....#..#.#..#....#.#....#..#....#.#..#.
###..####.###....#..#....#..#....#.#..#.
#....#..#.#..#..#...#....###.....#.###..
#....#..#.#..#.#....#....#.#..#..#.#.#..
####.#..#.###..####.####.#..#..##..#..#.";

        Assert.Equal(expectedOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).JoinWithNewLine(), output);
    }
}
