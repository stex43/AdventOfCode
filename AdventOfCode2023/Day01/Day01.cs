using Common;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day01;

[TestFixture]
public sealed class Day01 : Solver
{
    public Day01() 
        : base(nameof(Day01).Substring(3, 2))
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

            var firstDigit = -1;
            var secondDigit = -1;

            foreach (var symbol in line!)
            {
                if (!char.IsDigit(symbol))
                {
                    continue;
                }

                if (firstDigit == -1)
                {
                    firstDigit = ParseExtensions.ParseDigit(symbol);
                }
                
                secondDigit = ParseExtensions.ParseDigit(symbol);
            }
            
            var calibrationValue = firstDigit * 10 + secondDigit;
            sum += calibrationValue;
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(55017);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        long sum = 0;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            var firstDigit = -1;
            var secondDigit = -1;
            var possibleDigit = string.Empty;

            for (var i = 0; i < line!.Length; i++)
            {
                var symbol = line[i];
                
                if (char.IsDigit(symbol))
                {
                    if (firstDigit == -1)
                    {
                        firstDigit = ParseExtensions.ParseDigit(symbol);
                    }
                
                    secondDigit = ParseExtensions.ParseDigit(symbol);
                    continue;
                }

                var left = i;
                for (var right = i; right < line.Length; right++)
                {
                    symbol = line[right];
                    if (char.IsDigit(symbol))
                    {
                        possibleDigit = string.Empty;
                        i = right - 1;
                        break;
                    }
                    
                    possibleDigit += symbol;

                    var wordDigits = Dictionaries.WordToDigit.Keys;
                    if (wordDigits.Any(x => x.StartsWith(possibleDigit)))
                    {
                        if (Dictionaries.WordToDigit.TryGetValue(possibleDigit, out var digit))
                        {
                            if (firstDigit == -1)
                            {
                                firstDigit = digit;
                            }

                            secondDigit = digit;
                            i = right - 1;
                            possibleDigit = string.Empty;
                            break;
                        }
                        
                        continue;
                    }

                    possibleDigit = string.Empty;
                    left++;
                    right = left - 1;
                    i = right;
                }
            }
            
            var calibrationValue = firstDigit * 10 + secondDigit;
            sum += calibrationValue;
        }
        
        Console.WriteLine(sum);
        sum.Should().Be(53539);
    }
}