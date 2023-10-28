namespace Adventure.Net.Utilities;

public static class Random
{
    public static int Number(int min, int max)
    {
        return System.Random.Shared.Next(min, max);
    }
}
