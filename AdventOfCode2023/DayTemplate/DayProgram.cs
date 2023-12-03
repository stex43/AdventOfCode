using NUnit.Framework;

namespace AdventOfCode.DayTemplate;

[TestFixture]
public sealed class Day00 : Solver
{
    public Day00() 
        : base(nameof(Day00).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            
            
        }
        
        Console.WriteLine();
        
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            
            
        }
        
        Console.WriteLine();

    }
}