using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Gain Hp Effect",
    fileName = "GainHpEffect",
    order = 5)]
public class GainHpEffect : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return $"Gain {value.ToString()} Hp";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetHp = target.hp;
        var finalHp = targetHp.Value + value;

        if (finalHp > target.maxHp.Value)
        {
            Debug.Log("final hp is max hp now");
            finalHp = target.maxHp.Value;
        }
        
        target.hp.setValue(finalHp);
        Debug.Log("Gain Hp");
    }
}
