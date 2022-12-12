namespace AdventOfCode2022;

public class Day08 : IStringInputIntegerOutputPuzzle
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
        var width = trees.GetLength(0);
        var height = trees.GetLength(1);

        return
            IsVisibleFromNorth(trees, rowIndex, columnIndex, height)
            || IsVisibleFromEast(trees, rowIndex, columnIndex, width)
            || IsVisibleFromSouth(trees, rowIndex, columnIndex, height)
            || IsVisibleFromWest(trees, rowIndex, columnIndex, width);
    }

    private static int GetScenicScore(int[,] trees, int rowIndex, int columnIndex)
    {
        var width = trees.GetLength(0);
        var height = trees.GetLength(1);

        var tree = trees[rowIndex, columnIndex];

        return
            GetViewingDistance(GetTreesNorth(trees, rowIndex, columnIndex, height), tree)
            * GetViewingDistance(GetTreesEast(trees, rowIndex, columnIndex, width), tree)
            * GetViewingDistance(GetTreesSouth(trees, rowIndex, columnIndex, height), tree)
            * GetViewingDistance(GetTreesWest(trees, rowIndex, columnIndex, width), tree);
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

    private static bool IsVisibleFromNorth(int[,] trees, int rowIndex, int columnIndex, int height) => IsTallerThanTrees(GetTreesNorth(trees, rowIndex, columnIndex, height), trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromEast(int[,] trees, int rowIndex, int columnIndex, int width) => IsTallerThanTrees(GetTreesEast(trees, rowIndex, columnIndex, width), trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromSouth(int[,] trees, int rowIndex, int columnIndex, int height) => IsTallerThanTrees(GetTreesSouth(trees, rowIndex, columnIndex, height), trees[rowIndex, columnIndex]);
    private static bool IsVisibleFromWest(int[,] trees, int rowIndex, int columnIndex, int width) => IsTallerThanTrees(GetTreesWest(trees, rowIndex, columnIndex, width), trees[rowIndex, columnIndex]);

    private static int[] GetTreesNorth(int[,] trees, int rowIndex, int columnIndex, int height) => GetIndexRange(height)[..rowIndex].Select(x => trees[x, columnIndex]).Reverse().ToArray();
    private static int[] GetTreesEast(int[,] trees, int rowIndex, int columnIndex, int width) => GetIndexRange(width)[(columnIndex + 1)..].Select(x => trees[rowIndex, x]).ToArray();
    private static int[] GetTreesSouth(int[,] trees, int rowIndex, int columnIndex, int height) => GetIndexRange(height)[(rowIndex + 1)..].Select(x => trees[x, columnIndex]).ToArray();
    private static int[] GetTreesWest(int[,] trees, int rowIndex, int columnIndex, int width) => GetIndexRange(width)[..columnIndex].Select(x => trees[rowIndex, x]).Reverse().ToArray();

    private static int[] GetIndexRange(int count) => Enumerable.Range(0, count).ToArray();

    private static bool IsTallerThanTrees(IEnumerable<int> trees, int tree) => trees.All(x => x < tree);

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
