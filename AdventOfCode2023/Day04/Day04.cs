using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day04;

[TestFixture]
public sealed class Day04 : Solver
{
    public Day04() 
        : base(nameof(Day04).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        long sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            line = line!.Split(": ")[1];

            var split = line.Split(" | ");
            var winningNumbers = split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var ourNumbers = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var winningSet = winningNumbers.Select(int.Parse).ToHashSet();
            var ourSet = ourNumbers.Select(int.Parse);

            long score = 0;
            foreach (var number in ourSet)
            {
                if (!winningSet.Contains(number))
                {
                    continue;
                }

                if (score == 0)
                {
                    score = 1;
                    continue;
                }

                score *= 2;
            }

            sum += score;
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(20407);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var cards = new List<Card>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            line = line!.Split(": ")[1];

            var split = line.Split(" | ");
            var winningNumbers = split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var ourNumbers = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var winningSet = winningNumbers.Select(int.Parse).ToHashSet();
            var ourSet = ourNumbers.Select(int.Parse).ToArray();

            var card = new Card {TotalAmount = 1};
            cards.Add(card);
            
            foreach (var number in ourSet)
            {
                if (!winningSet.Contains(number))
                {
                    continue;
                }

                card.PriseCardsAmount++;
            }
        }

        for (var i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            if (card.PriseCardsAmount == 0)
            {
                continue;
            }

            while (card.ProcessedAmount != card.TotalAmount)
            {
                for (var j = 1; j <= card.PriseCardsAmount; j++)
                {
                    if (i + j >= cards.Count)
                    {
                        break;
                    }

                    cards[i + j].TotalAmount++;
                }
                
                card.ProcessedAmount++;
            }
        }

        var result = cards.Sum(x => x.TotalAmount);
        Console.WriteLine(result);
        result.Should().Be(23806951);
    }
    
    private class Card
    {
        public int TotalAmount { get; set; }
        
        public int ProcessedAmount { get; set; }
        
        public int PriseCardsAmount { get; set; }
    }
}