using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedRandom : MonoBehaviour
{
    private int seed;
    private int max;

    public SeedRandom(int seed, int max = 10)
    {
        this.seed = (seed != 0 ? seed : (int)DateTime.Now.Ticks) % 999999999;
        this.max = max;
    }

    public int Next(int max = 0)
    {
        max = max != 0 ? max : this.max;
        this.seed = (this.seed * 9301 + 49297) % 233280;
        double val = (double)this.seed / 233280.0;
        return (int)Math.Floor(val * max);
    }
}
