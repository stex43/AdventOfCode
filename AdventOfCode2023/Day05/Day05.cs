using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day05;

[TestFixture]
public sealed class Day05 : Solver
{
    public Day05() 
        : base(nameof(Day05).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var values = streamReader.ReadLine()!.Remove(0, 7).Split(" ").Select(ulong.Parse).ToList();
        streamReader.ReadLine();

        while (true)
        {
            var mapName = streamReader.ReadLine();

            var destinationValues = new List<ulong>();
            var line = streamReader.ReadLine();
            var map = new List<MapElement>();
            while (!string.IsNullOrEmpty(line))
            {
                var split = line.Split(" ");

                var mapElement = new MapElement
                {
                    DestinationRangeStart = ulong.Parse(split[0]),
                    SourceRangeStart = ulong.Parse(split[1]),
                    Length = ulong.Parse(split[2])
                };

                map.Add(mapElement);
                line = streamReader.ReadLine();
            }

            foreach (var value in values)
            {
                var mapped = false;
                foreach (var mapElement in map)
                {
                    if (value < mapElement.SourceRangeStart || value >= mapElement.SourceRangeStart + mapElement.Length)
                    {
                        continue;
                    }

                    var shift = value - mapElement.SourceRangeStart;
                    var destinationValue = mapElement.DestinationRangeStart + shift;
                    destinationValues.Add(destinationValue);
                    mapped = true;
                    break;
                }

                if (!mapped)
                {
                    destinationValues.Add(value);
                }
            }

            values = destinationValues;

            if (mapName.Contains("location"))
            {
                break;
            }
        }

        var result = values.Min();
        Console.WriteLine(result);
        result.Should().Be(806029445);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var seeds = streamReader.ReadLine()!.Remove(0, 7).Split(" ").Select(ulong.Parse).ToList();
        var values = new List<Tuple<ulong, ulong>>();
        for (var i = 0; i < seeds.Count; i++)
        {
            var start = seeds[i++];
            var length = seeds[i];
            values.Add(Tuple.Create(start, length));
        }
        
        streamReader.ReadLine();

        while (true)
        {
            var mapName = streamReader.ReadLine();
        
            var destinationValues = new List<Tuple<ulong, ulong>>();
            var line = streamReader.ReadLine();
            var map = new List<MapElement>();
            while (!string.IsNullOrEmpty(line))
            {
                var split = line.Split(" ");
        
                var mapElement = new MapElement
                {
                    DestinationRangeStart = ulong.Parse(split[0]),
                    SourceRangeStart = ulong.Parse(split[1]),
                    Length = ulong.Parse(split[2])
                };
        
                map.Add(mapElement);
                line = streamReader.ReadLine();
            }
        
            for (var i = 0; i < values.Count; i++)
            {
                var value = values[i];
                var start = value.Item1;
                var end = value.Item1 + value.Item2 - 1;
                var mapped = false;
                foreach (var mapElement in map)
                {
                    if ((start < mapElement.SourceRangeStart && end < mapElement.SourceRangeStart)
                        || (start >= mapElement.SourceRangeStart + mapElement.Length && end >= mapElement.SourceRangeStart + mapElement.Length))
                    {
                        continue;
                    }
                    
                    var mapElementSourceRangeEnd = mapElement.SourceRangeStart + mapElement.Length - 1;
                    var valueRangeEnd = value.Item1 + value.Item2 - 1;
                    if (start >= mapElement.SourceRangeStart && start < mapElement.SourceRangeStart + mapElement.Length)
                    {
                        var shift = start - mapElement.SourceRangeStart;
                        var destinationStart = mapElement.DestinationRangeStart + shift;
                        ulong length = 0;
                        
                        if (end >= mapElement.SourceRangeStart && end < mapElement.SourceRangeStart + mapElement.Length)
                        {
                            length = value.Item2;
                        }
                        else
                        {
                            length = value.Item2 - (valueRangeEnd - mapElementSourceRangeEnd);
                            
                            values.Add(Tuple.Create(mapElementSourceRangeEnd + 1, valueRangeEnd - mapElementSourceRangeEnd));
                        }
                        
                        destinationValues.Add(Tuple.Create(destinationStart, length));
                        mapped = true;
                        break;
                    }

                    if (end >= mapElement.SourceRangeStart && end < mapElement.SourceRangeStart + mapElement.Length)
                    {
                        var length = valueRangeEnd - mapElement.SourceRangeStart + 1;
                        
                        destinationValues.Add(Tuple.Create(mapElement.DestinationRangeStart, length));
                        values.Add(Tuple.Create(value.Item1, value.Item2 - length));
                        mapped = true;
                        break;
                    }
                    
                    destinationValues.Add(Tuple.Create(mapElement.DestinationRangeStart, mapElement.Length));
                    values.Add(Tuple.Create(value.Item1, mapElement.SourceRangeStart - value.Item1));
                    values.Add(Tuple.Create(mapElementSourceRangeEnd + 1, valueRangeEnd - mapElementSourceRangeEnd));
                    mapped = true;
                    break;
                }
        
                if (!mapped)
                {
                    destinationValues.Add(value);
                }
            }
        
            values = destinationValues;
        
            if (mapName.Contains("location"))
            {
                break;
            }
        }

        var result = values.Min(x => x.Item1);
        Console.WriteLine(result);
        result.Should().Be(59370572);
    }
    
    private class MapElement
    {
        public ulong DestinationRangeStart { get; set; }
        
        public ulong SourceRangeStart { get; set; }
        
        public ulong Length { get; set; }
    }
}