namespace AdventOfCode;

public abstract class Solver
{
    private readonly string dayNumber;
    
    protected Solver(string dayNumber)
    {
        this.dayNumber = dayNumber;
    }

    protected StreamReader GetStreamReader()
    {
        return new StreamReader($"Day{this.dayNumber}/input.txt");
    }
}