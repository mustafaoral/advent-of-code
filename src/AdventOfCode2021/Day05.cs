namespace AdventOfCode2021;

public class Day05 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input) => GetOutput(input, ventSegments => ventSegments.Where(x => !x.IsDiagonal));

    public int Part2(string input) => GetOutput(input, x => x);

    private static int GetOutput(string input, Func<IEnumerable<VentSegment>, IEnumerable<VentSegment>> filter)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var ventSegments = GetVentSegments(lines);
        var oceanFloor = GetOceanFloor(ventSegments);

        foreach (var ventSegment in filter(ventSegments))
        {
            oceanFloor.ApplyVentSegment(ventSegment);
        }

        return oceanFloor.GetOverlapCount();
    }

    private static OceanFloor GetOceanFloor(VentSegment[] ventSegments)
    {
        var xMax = ventSegments.Max(x => x.XMax);
        var yMax = ventSegments.Max(x => x.YMax);

        return new OceanFloor(yMax + 1, xMax + 1);
    }

    private class OceanFloor
    {
        public int[,] _grid;

        public OceanFloor(int height, int width)
        {
            _grid = new int[height, width];
        }

        public void ApplyVentSegment(VentSegment ventSegment)
        {
            foreach (var position in ventSegment.GetPositions())
            {
                _grid[position.Y, position.X]++;
            }
        }

        public int GetOverlapCount() => _grid.Cast<int>().Count(x => x >= 2);
    }

    private static VentSegment[] GetVentSegments(string[] lines)
    {
        return lines.Select(line =>
        {
            var matches = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();

            return new VentSegment(matches[0], matches[1], matches[2], matches[3]);
        }).ToArray();
    }

    private class VentSegment
    {
        private int[] _xRange { get; }
        private int[] _yRange { get; }

        public int XMax { get; }
        public int YMax { get; }

        public bool IsDiagonal { get; }

        public VentSegment(int x1, int y1, int x2, int y2)
        {
            var xMin = Math.Min(x1, x2);
            var xRange = Enumerable.Range(xMin, Math.Max(x1, x2) - xMin + 1);

            if (x1 > x2)
            {
                _xRange = xRange.Reverse().ToArray();
            }
            else
            {
                _xRange = xRange.ToArray();
            }

            var yMin = Math.Min(y1, y2);
            var yRange = Enumerable.Range(yMin, Math.Max(y1, y2) - yMin + 1);

            if (y1 > y2)
            {
                _yRange = yRange.Reverse().ToArray();
            }
            else
            {
                _yRange = yRange.ToArray();
            }

            XMax = Math.Max(x1, x2);
            YMax = Math.Max(y1, y2);

            IsDiagonal = _xRange.Length != 1 && _yRange.Length != 1;
        }

        public Position[] GetPositions()
        {
            if (IsDiagonal)
            {
                return _xRange.Zip(_yRange).Select(x => new Position(x.First, x.Second)).ToArray();
            }

            if (_xRange.Length == 1)
            {
                return _yRange.Select(x => new Position(_xRange[0], x)).ToArray();
            }

            return _xRange.Select(x => new Position(x, _yRange[0])).ToArray();
        }
    }

    private record Position(int X, int Y);
}
