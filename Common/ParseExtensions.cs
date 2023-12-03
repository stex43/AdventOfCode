using System.Collections.Immutable;

namespace Common;

public static class ParseExtensions
{
    public static int ParseDigit(char symbol)
    {
        if (!char.IsDigit(symbol))
        {
            throw new ArgumentException($"Symbol {symbol} is not a digit");
        }

        return symbol - '0';
    }
}