namespace Common;

public static class Arrays
{
    public static IEnumerable<Tuple<int, int>> GetNeighbours(int i, int j, int size)
    {
        if (i >= size || j >= size || i < 0 || j < 0)
        {
            throw new ArgumentOutOfRangeException($"The element ({i}, {j}) is not a part of the array");
        }
        
        var bottomBound = i - 1 >= 0 ? i - 1 : i;
        var upperBound = i + 1 < size ? i + 1 : i;
        var leftBound = j - 1 >= 0 ? j - 1 : j;
        var rightBound = j + 1 < size ? j + 1 : j;

        for (var k = bottomBound; k <= upperBound; k++)
        {
            for (var l = leftBound; l <= rightBound; l++)
            {
                if (k == i && l == j)
                {
                    continue;
                }
                
                yield return Tuple.Create(k, l);
            }
        }
    }
}