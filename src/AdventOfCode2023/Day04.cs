namespace AdventOfCode2023;

public class Day04 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var cards = GetCards(input);

        return cards.Sum(x =>
        {
            if (x.NumberOfMatches == 0)
            {
                return 0;
            }

            return (int)Math.Pow(2, x.NumberOfMatches - 1);
        });
    }

    public int Part2(string input)
    {
        var cards = GetCards(input);

        foreach (var indexedCard in cards.SelectIndexed())
        {
            for (int i = 0; i < indexedCard.Item.Copies; i++)
            {
                for (var j = 0; j < indexedCard.Item.NumberOfMatches; j++)
                {
                    cards[indexedCard.Index + 1 + j].AddCopy();
                }
            }
        }

        return cards.Sum(x => x.Copies);
    }

    private static Card[] GetCards(string input)
    {
        return input.Split(Environment.NewLine)
            .Select(line =>
            {
                var sides = line.Split(":")[1].Split("|");

                var winningNumbers = Regex.Matches(sides[0], @"\d+").Select(x => int.Parse(x.Value)).ToArray();
                var numbers = Regex.Matches(sides[1], @"\d+").Select(x => int.Parse(x.Value)).ToArray();

                return new Card(winningNumbers, numbers);
            })
            .ToArray();
    }

    private class Card(int[] winningNumbers, int[] numbers)
    {
        public int NumberOfMatches { get; private set; } = winningNumbers.Intersect(numbers).Count();
        public int Copies { get; private set; } = 1;

        public void AddCopy()
        {
            Copies++;
        }
    }
}
