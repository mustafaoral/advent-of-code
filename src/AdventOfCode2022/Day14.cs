namespace AdventOfCode2022;

public class Day14 : IStringInputPuzzle<int, int>
{
    public int Part1(string input) => Run(input, isPart2: false, endStateCondition: x => x.SandOverflown);
    public int Part2(string input) => Run(input, isPart2: true, endStateCondition: x => x.EntranceBlocked);

    private static int Run(string input, bool isPart2, Predicate<Cave> endStateCondition)
    {
        var cave = Cave.Parse(input, isPart2);

        for (int i = 0; i < int.MaxValue; i++)
        {
            cave.IntroduceSand();

            if (endStateCondition(cave))
            {
                return i;
            }
        }

        return -1;
    }

    private class Cave
    {
        private char[,] _grid;
        private int _sandHoleIndex;
        private int _width;

        public bool SandOverflown { get; private set; }
        public bool EntranceBlocked { get; private set; }

        public static Cave Parse(string input, bool isPart2 = false)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var matches = lines.SelectMany(x => Regex.Matches(x, @"(\d+),(\d+)")).ToArray();

            var columnIndices = matches.Select(x => int.Parse(x.Groups[1].Value));

            var minColumnIndex = columnIndices.Min();
            var maxColumnIndex = columnIndices.Max();

            var caveHeight = matches.Select(x => int.Parse(x.Groups[2].Value)).Max() + 1;
            var caveWidth = maxColumnIndex - minColumnIndex + 1;
            var columnOffset = 0;

            if (isPart2)
            {
                caveHeight += 2;
                caveWidth = 1000;
                columnOffset = 500;
            }

            var cave = new Cave
            {
                _grid = new char[caveHeight, caveWidth],
                _sandHoleIndex = 500 - minColumnIndex + columnOffset,
                _width = caveWidth
            };

            cave._grid.Fill('.');

            if (isPart2)
            {
                cave._grid.FillRow(caveHeight - 1, '#');
            }

            foreach (var line in lines)
            {
                var lineMatches = Regex.Matches(line, @"(\d+),(\d+)");

                foreach (var (start, end) in lineMatches.Pairwise())
                {
                    var startRowIndex = int.Parse(start.Groups[2].Value);
                    var endRowIndex = int.Parse(end.Groups[2].Value);
                    var minRow = Math.Min(startRowIndex, endRowIndex);
                    var height = Math.Abs(startRowIndex - endRowIndex) + 1;

                    var startColumnIndex = int.Parse(start.Groups[1].Value) + columnOffset;
                    var endColumnIndex = int.Parse(end.Groups[1].Value) + columnOffset;
                    var minColumn = Math.Min(startColumnIndex, endColumnIndex);
                    var width = Math.Abs(endColumnIndex - startColumnIndex) + 1;

                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            cave._grid[minRow + i, minColumn - minColumnIndex + j] = '#';
                        }
                    }
                }
            }

            return cave;
        }

        public void IntroduceSand()
        {
            var rowIndexOfBlockage = _grid.GetColumn(_sandHoleIndex).AsSpan().IndexOfAny('#', '@');
            var columnIndex = _sandHoleIndex;

            while (true)
            {
                if (GrainOnEdge(columnIndex))
                {
                    SandOverflown = true;

                    return;
                }

                if (rowIndexOfBlockage == 0 && columnIndex == _sandHoleIndex)
                {
                    EntranceBlocked = true;

                    return;
                }

                var column = _grid.GetColumn(columnIndex);

                rowIndexOfBlockage += column.AsSpan()[rowIndexOfBlockage..].IndexOfAny('#', '@');

                if (!IsBlocked(rowIndexOfBlockage, columnIndex - 1))
                {
                    columnIndex--;
                    rowIndexOfBlockage++;
                }
                else if (!IsBlocked(rowIndexOfBlockage, columnIndex + 1))
                {
                    columnIndex++;
                    rowIndexOfBlockage++;
                }
                else
                {
                    _grid[rowIndexOfBlockage - 1, columnIndex] = '@';

                    break;
                }
            }
        }

        private bool GrainOnEdge(int columnIndex) => columnIndex == 0 || columnIndex + 1 == _width;

        public bool IsBlocked(int rowIndex, int columnIndex)
        {
            var value = _grid[rowIndex, columnIndex];

            return value == '#' || value == '@';
        }
    }
}
