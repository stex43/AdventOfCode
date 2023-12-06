using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day06;

[TestFixture]
public sealed class Day06 : Solver
{
    public Day06() 
        : base(nameof(Day06).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var line = streamReader.ReadLine();
        var times = line!.Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        line = streamReader.ReadLine();
        var distances = line!.Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

        var result = 1;

        for (var race = 0; race < times.Length; race++)
        {
            var raceTime = times[race];
            var raceRecordDistance = distances[race];
            var win = 0;

            for (var holdTime = 0; holdTime < raceTime; holdTime++)
            {
                var leftTime = raceTime - holdTime;
                var speed = holdTime;
                var distance = speed * leftTime;

                win += distance > raceRecordDistance ? 1 : 0;
            }

            result *= win;
        }
        
        Console.WriteLine(result);
        result.Should().Be(1155175);
    }

    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var line = streamReader.ReadLine();
        var times = line!.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1);
        var raceTime = ulong.Parse(string.Join(string.Empty, times));
        line = streamReader.ReadLine();
        var distances = line!.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Skip(1);
        var raceRecordDistance = ulong.Parse(string.Join(string.Empty, distances));
        
        var win = 0;

        for (ulong holdTime = 0; holdTime < raceTime; holdTime++)
        {
            var leftTime = raceTime - holdTime;
            var speed = holdTime;
            var distance = speed * leftTime;

            win += distance > raceRecordDistance ? 1 : 0;
        }

        Console.WriteLine(win);
        win.Should().Be(35961505);
    }
}