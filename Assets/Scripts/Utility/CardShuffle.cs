using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CardShuffle
{
    private static readonly System.Random _random  = new System.Random();

    public static void shuffle<T>(this List<T> list)
    {
        var n = list.Count;
        while (n--> 1)
        {
            var index = _random.Next(n+1);
            var value = list[index];
            list[index] = list[n];
            list[n] = value;
        }
    }
}
