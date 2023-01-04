using System.Diagnostics;

namespace AdventOfCode2022;

public class Day12 : IStringInputPuzzle<int, int>
{
    public int Part1(string input)
    {
        var map = Map.Parse(input);

        var startPosition = GetStartingPositionFromInput(input);

        return map.GetShortestPathLengthToSummit(startPosition);
    }

    public int Part2(string input)
    {
        var map = Map.Parse(input);

        return map.GetPositionsWithElevation('a').Aggregate(seed: new List<int>(), (accumulate, startingPosition) =>
        {
            accumulate.Add(map.GetShortestPathLengthToSummit(startingPosition));

            map.ClearHikingHistory();

            return accumulate;
        }, accumulate => accumulate.Where(x => x > 0).Min());
    }

    private static Position GetStartingPositionFromInput(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var width = lines[0].Length;

        var index = Array.IndexOf(lines.SelectMany(x => x).ToArray(), 'S');

        return new Position(index / width, index % width, 'a');
    }

    private class Map
    {
        private int _height;
        private int _width;
        private char[,] _heightMap;
        private bool[,] _visited;
        private Position _endPosition;

        public static Map Parse(string input)
        {
            var lines = input.Replace('S', 'a').Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var map = new Map
            {
                _height = lines.Length,
                _width = lines[0].Length,
                _heightMap = new char[lines.Length, lines[0].Length],
                _visited = new bool[lines.Length, lines[0].Length]
            };

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    var value = line[j];

                    if (value == 'E')
                    {
                        value = 'z';

                        map._endPosition = new Position(i, j, value);
                    }

                    map._heightMap[i, j] = value;
                }
            }

            return map;
        }

        public int GetShortestPathLengthToSummit(Position startingPosition)
        {
            var queue = new Queue<Position>();

            queue.Enqueue(startingPosition);

            while (queue.TryDequeue(out var position))
            {
                if (position.RowIndex == _endPosition.RowIndex && position.ColumnIndex == _endPosition.ColumnIndex)
                {
                    return position.GetCountOfAllAncestors();
                }

                var nextPositions = GetNextPositions(position);

                foreach (var nextPosition in nextPositions)
                {
                    _visited[nextPosition.RowIndex, nextPosition.ColumnIndex] = true;

                    nextPosition.Ancestor = position;

                    queue.Enqueue(nextPosition);
                }
            }

            return -1;
        }

        public IEnumerable<Position> GetPositionsWithElevation(char elevation)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_heightMap[i, j] == elevation)
                    {
                        yield return new Position(i, j, _heightMap[i, j]);
                    }
                }
            }
        }

        public void ClearHikingHistory() => _visited.Fill(false);

        private Position[] GetNextPositions(Position position)
        {
            var candidates = new List<Position>();

            if (position.RowIndex < _height - 1)
            {
                candidates.Add(new Position(position.RowIndex + 1, position.ColumnIndex, _heightMap[position.RowIndex + 1, position.ColumnIndex]));
            }

            if (position.RowIndex > 0)
            {
                candidates.Add(new Position(position.RowIndex - 1, position.ColumnIndex, _heightMap[position.RowIndex - 1, position.ColumnIndex]));
            }

            if (position.ColumnIndex < _width - 1)
            {
                candidates.Add(new Position(position.RowIndex, position.ColumnIndex + 1, _heightMap[position.RowIndex, position.ColumnIndex + 1]));
            }

            if (position.ColumnIndex > 0)
            {
                candidates.Add(new Position(position.RowIndex, position.ColumnIndex - 1, _heightMap[position.RowIndex, position.ColumnIndex - 1]));
            }

            return candidates
                .Where(x => !_visited[x.RowIndex, x.ColumnIndex])
                .Where(x => x.Value - position.Value <= 1)
                .ToArray();
        }
    }

    [DebuggerDisplay("{DebuggerDisplayValue,nq}")]
    private class Position
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplayValue => $"{RowIndex}-{ColumnIndex} | {Value}";

        public Position Ancestor { get; set; }
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public char Value { get; }

        public Position(int rowIndex, int columnIndex, char value)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Value = value;
        }

        public int GetCountOfAllAncestors()
        {
            var count = 0;
            var ancestor = Ancestor;

            while (ancestor != null)
            {
                count++;

                ancestor = ancestor.Ancestor;
            }

            return count;
        }
    }
}
