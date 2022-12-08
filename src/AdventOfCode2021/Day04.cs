namespace AdventOfCode2021;

public class Day04 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var numbers = lines[0].Split(",").Select(int.Parse);
        var boards = GetBoards(lines);

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                board.Mark(number);

                if (board.IsWinning())
                {
                    return board.GetScore(number);
                }
            }
        }

        return -1;
    }

    public int Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var numbers = lines[0].Split(",").Select(int.Parse);
        var boards = GetBoards(lines);

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                board.Mark(number);

                if (boards.All(x => x.IsWinning()))
                {
                    return board.GetScore(number);
                }
            }
        }

        return -1;
    }

    private static Board[] GetBoards(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Chunk(5)
            .Select(Board.Parse)
            .ToArray();
    }

    private class Board
    {
        private int[,] _numbers = new int[5, 5];
        private bool[,] _hits = new bool[5, 5];
        private Dictionary<int, (int RowIndex, int ColumnIndex)> _reverseIndex = new();

        public static Board Parse(IEnumerable<string> boardLines)
        {
            var boardNumbers = boardLines
                .SelectMany(line => Regex.Matches(line, @"\d+").Select(x => x.Value))
                .Select(int.Parse)
                .ToArray();

            var board = new Board();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var item = boardNumbers[i * 5 + j];

                    board._numbers[i, j] = item;
                    board._reverseIndex[item] = (i, j);
                }
            }

            return board;
        }

        public void Mark(int number)
        {
            if (_reverseIndex.TryGetValue(number, out var index))
            {
                _hits[index.RowIndex, index.ColumnIndex] = true;
            }
        }

        public bool IsWinning()
        {
            for (int i = 0; i < 5; i++)
            {
                if (_hits.GetColumn(i).All(x => x) || _hits.GetRow(i).All(x => x))
                {
                    return true;
                }
            }

            if (_hits.GetDiagonal(increasingIndex: true).All(x => x) || _hits.GetDiagonal(increasingIndex: false).All(x => x))
            {
                return true;
            }

            return false;
        }

        public int GetScore(int number)
        {
            return _hits
                .Cast<bool>()
                .Zip(_numbers.Cast<int>())
                .Where(x => !x.First)
                .Sum(x => x.Second) * number;
        }
    }
}
