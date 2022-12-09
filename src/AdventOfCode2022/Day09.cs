namespace AdventOfCode2022;

public class Day09 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var moves = GetMoves(lines);

        var headPosition = new HeadPosition();
        var tailPosition = new TailPosition();
        var tailPositionHistory = new HashSet<Position>();

        foreach (var move in moves)
        {
            for (int i = 0; i < move.Count; i++)
            {
                headPosition.ApplyMove(move);
                tailPosition.FollowHeadPosition(headPosition);

                tailPositionHistory.Add(tailPosition.ClonePosition());
            }
        }

        return tailPositionHistory.Count;
    }

    public int Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();

        var moves = GetMoves(lines);

        var headPosition = new HeadPosition();
        var tailPositions = Enumerable.Range(0, 9).Select(_ => new TailPosition()).ToArray();
        var tailPositionHistory = new HashSet<Position>();

        foreach (var move in moves)
        {
            for (int i = 0; i < move.Count; i++)
            {
                headPosition.ApplyMove(move);
                tailPositions.First().FollowHeadPosition(headPosition);

                foreach (var window in tailPositions.SlidingWindow(2))
                {
                    window.ElementAt(1).FollowHeadPosition(window.ElementAt(0));
                }

                tailPositionHistory.Add(tailPositions.Last().ClonePosition());
            }
        }

        return tailPositionHistory.Count;
    }

    private static Move[] GetMoves(string[] lines) => lines.Select(x => new Move(Direction: x[0], Count: int.Parse(x[1..]))).ToArray();

    private class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Position other) => X == other.X && Y == other.Y;

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
    }

    private class HeadPosition : Position
    {
        public void ApplyMove(Move move)
        {
            switch (move.Direction)
            {
                case 'R':
                    X++;
                    break;
                case 'L':
                    X--;
                    break;
                case 'U':
                    Y++;
                    break;
                case 'D':
                    Y--;
                    break;
            }
        }
    }

    private class TailPosition : Position
    {
        public void FollowHeadPosition(Position headPosition)
        {
            var hDiff = headPosition.X - X;
            var vDiff = headPosition.Y - Y;

            MoveHorizontal(hDiff, vDiff);
            MoveVertical(hDiff, vDiff);
        }

        private void MoveHorizontal(int hDiff, int vDiff)
        {
            if (hDiff == 0 || Math.Abs(hDiff) <= 1 && Math.Abs(vDiff) <= 1)
            {
                return;
            }

            if (hDiff > 0)
            {
                X++;
            }
            else
            {
                X--;
            }
        }

        private void MoveVertical(int hDiff, int vDiff)
        {
            if (vDiff == 0 || Math.Abs(vDiff) <= 1 && Math.Abs(hDiff) <= 1)
            {
                return;
            }

            if (vDiff > 0)
            {
                Y++;
            }
            else
            {
                Y--;
            }
        }

        public Position ClonePosition() => new() { X = X, Y = Y };
    }

    private record Move(char Direction, int Count);
}
