using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Gain Shield Effect",
    fileName = "Gain Shield Effect")]

public class GainShieldEffect : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return $"Gain {value.ToString()} Shield";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetShield = target.shield;
        targetShield.setValue(targetShield.Value+value);
    }
}
