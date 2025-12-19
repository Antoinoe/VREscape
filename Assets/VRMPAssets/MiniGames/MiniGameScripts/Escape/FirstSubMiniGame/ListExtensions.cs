using System.Collections.Generic;
using Unity.Netcode;


public static class ListExtensions
{
    private static readonly System.Random _rng = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}