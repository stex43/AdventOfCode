using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day02;

[TestFixture]
public sealed class Day02 : Solver
{
    public Day02() 
        : base(nameof(Day02).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var dict = new Dictionary<string, int>()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };
        var sum = 0;

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            
            var split = line!.Split(": ");
            var gameId = int.Parse(split[0].Split(" ")[1]);

            var sets = split[1].Split("; ");
            var maybe = true;
            foreach (var set in sets)
            {
                var splitSet = set.Split(", ");

                foreach (var s in splitSet)
                {
                    var sss = s.Split(" ");
                    var amount = int.Parse(sss[0]);
                    var color = sss[1];

                    if (amount > dict[color])
                    {
                        maybe = false;
                        break;
                    }
                }


                if (!maybe)
                {
                    break;
                }
            }

            if (maybe)
            {
                sum += gameId;
            }
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(2256);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();
        var sum = 0;

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            
            var split = line!.Split(": ");

            var sets = split[1].Split("; ");
            var dict = new Dictionary<string, int>()
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };
            foreach (var set in sets)
            {
                var splitSet = set.Split(", ");

                foreach (var s in splitSet)
                {
                    var sss = s.Split(" ");
                    var amount = int.Parse(sss[0]);
                    var color = sss[1];
                    dict[color] = Math.Max(dict[color], amount);
                }
            }

            var power = dict.Values.Aggregate((x, y) => x * y);
            sum += power;
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(74229);
    }
}