using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Patterns/Random Pattern",
    fileName = "RandomPattern",
    order = 0)]
public class RandomPattern : Pattern
{
    public List<Probability> probabilities = new List<Probability>(4);
    
    public override string GetName()
    {
        return "Random";
    }
}
