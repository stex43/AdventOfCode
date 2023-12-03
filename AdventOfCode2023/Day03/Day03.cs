using Common;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day03;

[TestFixture]
public sealed class Day03 : Solver
{
    public Day03() 
        : base(nameof(Day03).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        char[,]? schematic = null;
        var size = 0;

        var lineCount = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            if (schematic == null)
            {
                size = line!.Length;
                schematic = new char[size, size];
            }

            var rowCount = 0;
            foreach (var symbol in line!)
            {
                schematic[lineCount, rowCount] = symbol;
                rowCount++;
            }

            lineCount++;
        }

        var sum = 0;

        for (var i = 0; i < size; i++)
        {
            var number = 0;
            var isPart = false;
            for (var j = 0; j < size; j++)
            {
                var symbol = schematic![i, j];
                if (!char.IsDigit(symbol))
                {
                    if (number != 0 && isPart)
                    {
                        sum += number;
                    }

                    number = 0;
                    isPart = false;
                    continue;
                }

                number = number * 10 + ParseExtensions.ParseDigit(symbol);

                foreach (var neighbourPosition in Arrays.GetNeighbours(i, j, size))
                {
                    var neighbour = schematic[neighbourPosition.Item1, neighbourPosition.Item2];

                    if (!char.IsDigit(neighbour) && neighbour != '.')
                    {
                        isPart = true;
                    }
                }
            }
            
            if (number != 0 && isPart)
            {
                sum += number;
            }
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(498559);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        char[,]? schematic = null;
        var size = 0;

        var lineCount = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            if (schematic == null)
            {
                size = line!.Length;
                schematic = new char[size, size];
            }

            var rowCount = 0;
            foreach (var symbol in line!)
            {
                schematic[lineCount, rowCount] = symbol;
                rowCount++;
            }

            lineCount++;
        }

        var numberList = new List<SchematicNumber>();

        for (var i = 0; i < size; i++)
        {
            var number = 0;
            for (var j = 0; j < size; j++)
            {
                var symbol = schematic![i, j];
                if (!char.IsDigit(symbol))
                {
                    if (number != 0)
                    {
                        var length = number.ToString().Length;
                        numberList.Add(new SchematicNumber
                        {
                            Number = number,
                            Line = i,
                            Row = j - length,
                            Length = length
                        });
                    }

                    number = 0;
                    continue;
                }

                number = number * 10 + ParseExtensions.ParseDigit(symbol);
            }

            numberList.Add(new SchematicNumber
            {
                Number = number,
                Line = i,
                Row = size - number.ToString().Length,
                Length = number.ToString().Length
            });
        }

        long sum = 0;
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var symbol = schematic![i, j];
                if (symbol != '*')
                {
                    continue;
                }

                var listParts = new List<int>();
                foreach (var schematicNumber in numberList)
                {
                    if (Math.Abs(i - schematicNumber.Line) > 1)
                    {
                        continue;
                    }

                    if (Math.Abs(j - schematicNumber.Row) <= 1 ||
                        Math.Abs(j - schematicNumber.Row - schematicNumber.Length + 1) <= 1 ||
                        (j >= schematicNumber.Row && j <= schematicNumber.Row + schematicNumber.Length - 1))
                    {
                        listParts.Add(schematicNumber.Number);
                    }
                }

                if (listParts.Count == 2)
                {
                    sum += (long)listParts[0] * (long)listParts[1];
                }
            }
        }
        
        Console.WriteLine(sum);

    }
    
    private class SchematicNumber
    {
        public int Number { get; set; }
        
        public int Length { get; set; }
        
        public int Row { get; set; }
        
        public int Line { get; set; }
    }
}