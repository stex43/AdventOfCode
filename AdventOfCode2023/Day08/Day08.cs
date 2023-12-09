using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day08;

[TestFixture]
public sealed class Day08 : Solver
{
    public Day08() 
        : base(nameof(Day08).Substring(3, 2))
    {
    }
    
    [Test]
    public void Part1()
    {
        using var streamReader = this.GetStreamReader();

        var instructions = streamReader.ReadLine();
        streamReader.ReadLine();
        
        var network = new Dictionary<string, Tuple<string, string>>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            var split = line!.Split(new[] { " = ", ", ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            network[split[0]] = Tuple.Create(split[1], split[2]);
        }

        var currentNode = "AAA";
        var step = 0;
        var i = 0;
        while (currentNode != "ZZZ")
        {
            var instruction = instructions![i];
            currentNode = instruction == 'L' ? network[currentNode].Item1 : network[currentNode].Item2;
            
            i = i + 1 == instructions!.Length ? 0 : i + 1;
            step++;
        }
        
        Console.WriteLine(step);
        step.Should().Be(17873);
    }
    
    [Test]
    public void Part2()
    {
        using var streamReader = this.GetStreamReader();

        var instructions = streamReader.ReadLine();
        streamReader.ReadLine();
        
        var network = new Dictionary<string, Tuple<string, string>>();
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            var split = line!.Split(new[] { " = ", ", ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            network[split[0]] = Tuple.Create(split[1], split[2]);
        }

        var currentNodes = network.Keys.Where(x => x.EndsWith('A')).ToList();
        int step = 0;
        var i = 0;
        var contact = new int[currentNodes.Count];
        //while (!currentNodes.TrueForAll(x => x.EndsWith('Z')))
        while (contact.Any(x => x == 0))
        {
            var instruction = instructions![i];
            var newNodes = new List<string>(currentNodes.Count);
            for (var index = 0; index < currentNodes.Count; index++)
            {
                var currentNode = currentNodes[index];
                var newNode = instruction == 'L' ? network[currentNode].Item1 : network[currentNode].Item2;

                if (newNode.EndsWith("Z") && contact[index] == 0)
                {
                    contact[index] = step + 1;
                }

                newNodes.Add(newNode);
            }

            i = i + 1 == instructions!.Length ? 0 : i + 1;
            step++;
            currentNodes = newNodes;
        }
        
        Console.WriteLine(step);
        // 15746133679061
    }
}