namespace AdventOfCode2022;

public class Day02
{
    public static int Part1(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(x =>
            {
                var tokens = x.Split(" ");

                return new MoveBasedPlay
                {
                    Opponent = MoveParser.ParseFromOpponent(tokens[0]),
                    Self = MoveParser.ParseFromSelf(tokens[1])
                };
            })
            .Select(x => x.GetScore())
            .Sum();
    }

    public static int Part2(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(x =>
            {
                var tokens = x.Split(" ");

                return new OutcomeBasedPlay
                {
                    Opponent = MoveParser.ParseFromOpponent(tokens[0]),
                    DesiredOutcome = OutcomeParser.Parse(tokens[1])
                };
            })
            .Select(x => x.GetScore())
            .Sum();
    }

    private class MoveBasedPlay
    {
        public Move Opponent { get; set; }
        public Move Self { get; set; }

        public int GetScore()
        {
            var outcome = Self.CompareTo(Opponent);

            return outcome.Score + Self.Score;
        }
    }

    private class OutcomeBasedPlay
    {
        public Move Opponent { get; set; }
        public Outcome DesiredOutcome { get; set; }

        public int GetScore()
        {
            var matchingMove = Opponent.GetMatchingMoveForDesiredOutcome(DesiredOutcome);

            return DesiredOutcome.Score + matchingMove.Score;
        }
    }

    private static class MoveParser
    {
        public static Move ParseFromOpponent(string s)
        {
            return s switch
            {
                "A" => Move.Rock,
                "B" => Move.Paper,
                "C" => Move.Scissors,
                _ => null
            };
        }

        public static Move ParseFromSelf(string s)
        {
            return s switch
            {
                "X" => Move.Rock,
                "Y" => Move.Paper,
                "Z" => Move.Scissors,
                _ => null
            };
        }
    }

    private static class OutcomeParser
    {
        public static Outcome Parse(string s)
        {
            return s switch
            {
                "X" => Outcome.Lose,
                "Y" => Outcome.Draw,
                "Z" => Outcome.Win,
                _ => null
            };
        }
    }

    private class Move
    {
        public int Score { get; }

        public static readonly Move Rock = new(1);
        public static readonly Move Paper = new(2);
        public static readonly Move Scissors = new(3);

        public Move(int score)
        {
            Score = score;
        }

        public Outcome CompareTo(Move other)
        {
            if (this == other)
            {
                return Outcome.Draw;
            }

            if (this == Rock && other == Scissors)
            {
                return Outcome.Win;
            }

            if (this == Paper && other == Rock)
            {
                return Outcome.Win;
            }

            if (this == Scissors && other == Paper)
            {
                return Outcome.Win;
            }

            return Outcome.Lose;
        }

        public Move GetMatchingMoveForDesiredOutcome(Outcome outcome)
        {
            if (outcome == Outcome.Win)
            {
                if (this == Rock)
                {
                    return Paper;
                }

                if (this == Paper)
                {
                    return Scissors;
                }

                return Rock;
            }

            if (outcome == Outcome.Lose)
            {
                if (this == Rock)
                {
                    return Scissors;
                }

                if (this == Paper)
                {
                    return Rock;
                }

                return Paper;
            }

            return this;
        }
    }

    private class Outcome
    {
        public int Score { get; }

        public static readonly Outcome Win = new(6);
        public static readonly Outcome Draw = new(3);
        public static readonly Outcome Lose = new(0);

        public Outcome(int score)
        {
            Score = score;
        }
    }
}
