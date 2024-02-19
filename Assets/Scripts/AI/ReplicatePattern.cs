using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Patterns/Replicate Pattern",
    fileName = "ReplicatePattern",
    order = 1)]
public class ReplicatePattern : Pattern
{
    public int times;
    public Sprite sprite;
    
    public override string GetName()
    {
        return $"Replicate x {times.ToString()}";
    }
}
