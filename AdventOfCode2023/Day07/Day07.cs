using System.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day07;

[TestFixture]
public sealed class Day07 : Solver
{
    private static List<char> Cards = new List<char>
        { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
    private static List<char> JokerCards = new List<char>
        { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

    public Day07() 
        : base(nameof(Day07).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var hands = new List<Hand>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            var split = line.Split();
            var handValue = split[0];
            var hand = new Hand
            {
                Value = handValue,
                Bid = int.Parse(split[1]),
                Type = this.GetType(handValue),
                Orderrrr = GetOrderrrrr(handValue)
            };
            
            hands.Add(hand);
        }

        var t = hands
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Orderrrr)
            .ToArray();

        long result = 0;
        for (var i = 0; i < t.Length; i++)
        {
            result += (i + 1) * t[i].Bid;
        }
        
        Console.WriteLine(result);
        result.Should().Be(249390788);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var hands = new List<Hand>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            var split = line.Split();
            var handValue = split[0];
            var hand = new Hand
            {
                Value = handValue,
                Bid = int.Parse(split[1]),
                Type = this.GetTypeJoker(handValue),
                Orderrrr = this.GetOrderrrrrJoker(handValue)
            };
            
            hands.Add(hand);
        }

        var t = hands
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Orderrrr)
            .ToArray();

        long result = 0;
        for (var i = 0; i < t.Length; i++)
        {
            result += (i + 1) * t[i].Bid;
        }
        
        Console.WriteLine(result);
        // 249049829 too high :( 
        // 248750248
    }

    private int GetType(string handValue)
    {
        var dict = new Dictionary<char, int>();
        foreach (var card in handValue)
        {
            dict.TryAdd(card, 0);
            dict[card]++;
        }

        if (dict.Count == 1)
        {
            return 7;
        }

        if (dict.Count == 2)
        {
            if (dict.ContainsValue(4))
            {
                return 6;
            }

            return 5;
        }

        if (dict.Count == 3)
        {
            if (dict.ContainsValue(3))
            {
                return 4;
            }

            // two pairs
            return 3;
        }

        if (dict.Count == 4)
        {
            return 2;
        }

        return 1;
    }
    
    private int GetTypeJoker(string handValue)
    {
        var dict = new Dictionary<char, int>();
        foreach (var card in handValue)
        {
            dict.TryAdd(card, 0);
            dict[card]++;
        }

        dict.TryGetValue('J', out var jokers);

        // five of a kind
        if (dict.Count == 1)
        {
            return 7;
        }

        if (dict.Count == 2)
        {
            // four of a kind
            if (dict.ContainsValue(4))
            {
                if (jokers == 1 || jokers == 4)
                {
                    return 7;
                }
                
                return 6;
            }

            // full house
            if (jokers == 2 || jokers == 3)
            {
                return 7;
            }
            
            return 5;
        }

        if (dict.Count == 3)
        {
            // three of a kind
            if (dict.ContainsValue(3))
            {
                if (jokers == 1 || jokers == 3)
                {
                    return 6;
                }
                
                return 4;
            }

            // two pairs
            if (jokers == 2)
            {
                return 6;
            }

            if (jokers == 1)
            {
                return 5;
            }
            
            return 3;
        }

        // one pair
        if (dict.Count == 4)
        {
            if (jokers == 1 || jokers == 2)
            {
                return 4;
            }
            
            return 2;
        }

        if (jokers == 1)
        {
            return 2;
        }

        return 1;
    }

    private int GetOrderrrrr(string handValue)
    {
        var result = 0;
        foreach (var t in handValue)
        {
            var index = Cards.IndexOf(t) + 1;
            var multi = Cards.Count - index;
            result *= 100;
            result += multi;
        }

        return result;
    }

    private int GetOrderrrrrJoker(string handValue)
    {
        var result = 0;
        foreach (var t in handValue)
        {
            var index = JokerCards.IndexOf(t) + 1;
            var multi = JokerCards.Count - index;
            result *= 100;
            result += multi;
        }

        return result;
    }
    
    private class Hand
    {
        public string Value { get; set; }
        
        public int Type { get; set; }
        
        public int Bid { get; set; }
        
        public int Orderrrr { get; set; }
    }
}