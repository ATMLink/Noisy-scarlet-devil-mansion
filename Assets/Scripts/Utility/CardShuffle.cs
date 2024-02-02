using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardShuffle
{
    private static readonly SeedRandom _random;

    public static void shuffle<T>(this List<T> list)
    {
        var n = list.Count;
        while (n > 0)
        {
            var index = _random.Next(n);
            var value = list[index];
            list[index] = list[n-1];
            list[n-1] = value;
            n--;
        }
    }
}
