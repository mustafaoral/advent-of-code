namespace AdventOfCode2022;

public class Day08Alternative : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var width = lines[0].Length;
        var height = lines.Length;

        var trees = GetTrees(lines, height, width);
        var visibilities = new bool[height, width];

        for (int rowIndex = 0; rowIndex < height; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < width; columnIndex++)
            {
                visibilities[rowIndex, columnIndex] = IsVisibleFromAnyDirection(trees, rowIndex, columnIndex);
            }
        }

        return visibilities.Cast<bool>().Count(x => x);
    }

    public int Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var width = lines[0].Length;
        var height = lines.Length;

        var trees = GetTrees(lines, height, width);
        var scenicScores = new int[height, width];

        for (int rowIndex = 0; rowIndex < height; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < width; columnIndex++)
            {
                scenicScores[rowIndex, columnIndex] = GetScenicScore(trees, rowIndex, columnIndex);
            }
        }

        return scenicScores.Cast<int>().Max();
    }

    private static bool IsVisibleFromAnyDirection(int[,] trees, int rowIndex, int columnIndex)
    {
        return
            IsVisibleFromNorth(trees, rowIndex, columnIndex)
            || IsVisibleFromEast(trees, rowIndex, columnIndex)
            || IsVisibleFromSouth(trees, rowIndex, columnIndex)
            || IsVisibleFromWest(trees, rowIndex, columnIndex);
    }

    private static int GetScenicScore(int[,] trees, int rowIndex, int columnIndex)
    {
        var tree = trees[rowIndex, columnIndex];

        return
            GetViewingDistance(GetTreesNorth(trees, rowIndex, columnIndex), tree)
            * GetViewingDistance(GetTreesEast(trees, rowIndex, columnIndex), tree)
            * GetViewingDistance(GetTreesSouth(trees, rowIndex, columnIndex), tree)
            * GetViewingDistance(GetTreesWest(trees, rowIndex, columnIndex), tree);
    }

    private static int GetViewingDistance(int[] trees, int tree)
    {
        var count = 0;

        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i] < tree)
            {
                count++;

                continue;
            }

            count++;

            break;
        }

        return count;
    }

    private static bool IsVisibleFromNorth(int[,] trees, int rowIndex, int columnIndex) => GetTreesNorth(trees, rowIndex, columnIndex).All(x => x < trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromEast(int[,] trees, int rowIndex, int columnIndex) => GetTreesEast(trees, rowIndex, columnIndex).All(x => x < trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromSouth(int[,] trees, int rowIndex, int columnIndex) => GetTreesSouth(trees, rowIndex, columnIndex).All(x => x < trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromWest(int[,] trees, int rowIndex, int columnIndex) => GetTreesWest(trees, rowIndex, columnIndex).All(x => x < trees[rowIndex, columnIndex]);

    private static int[] GetTreesNorth(int[,] trees, int rowIndex, int columnIndex) => trees.GetColumn(columnIndex)[..rowIndex].Reverse().ToArray();
    private static int[] GetTreesEast(int[,] trees, int rowIndex, int columnIndex) => trees.GetRow(rowIndex)[(columnIndex + 1)..];
    private static int[] GetTreesSouth(int[,] trees, int rowIndex, int columnIndex) => trees.GetColumn(columnIndex)[(rowIndex + 1)..];
    private static int[] GetTreesWest(int[,] trees, int rowIndex, int columnIndex) => trees.GetRow(rowIndex)[..columnIndex].Reverse().ToArray();

    private static int[,] GetTrees(string[] lines, int height, int width)
    {
        var trees = new int[height, width];

        for (int rowIndex = 0; rowIndex < height; rowIndex++)
        {
            var line = lines[rowIndex];

            for (int columnIndex = 0; columnIndex < width; columnIndex++)
            {
                trees[rowIndex, columnIndex] = int.Parse(line[columnIndex..(columnIndex + 1)]);
            }
        }

        return trees;
    }
}
