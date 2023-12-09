using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day09;

[TestFixture]
public sealed class Day09 : Solver
{
    public Day09() 
        : base(nameof(Day09).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            var sequences = new List<List<int>>();
            var initialNumbers = line.Split().Select(int.Parse).ToList();
            sequences.Add(initialNumbers);
            while (sequences[^1].Exists(x => x != 0))
            {
                var current = sequences[^1];
                var newSequence = new List<int>();
                for (var i = 0; i < current.Count - 1; i++)
                {
                    var diff = current[i + 1] - current[i];
                    newSequence.Add(diff);
                }
                
                sequences.Add(newSequence);
            }

            for (var i = sequences.Count - 2; i >= 0; i--)
            {
                var newValue = sequences[i][^1] + sequences[i + 1][^1];
                sequences[i].Add(newValue);
            }

            sum += sequences[0][^1];
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(1641934234);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            var sequences = new List<List<int>>();
            var initialNumbers = line.Split().Select(int.Parse).ToList();
            sequences.Add(initialNumbers);
            while (sequences[^1].Exists(x => x != 0))
            {
                var current = sequences[^1];
                var newSequence = new List<int>();
                for (var i = 0; i < current.Count - 1; i++)
                {
                    var diff = current[i + 1] - current[i];
                    newSequence.Add(diff);
                }
                
                sequences.Add(newSequence);
            }

            for (var i = sequences.Count - 2; i >= 0; i--)
            {
                var newValue = sequences[i][0] - sequences[i + 1][0];
                sequences[i].Insert(0, newValue);
            }

            sum += sequences[0][0];
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(975);
    }
}