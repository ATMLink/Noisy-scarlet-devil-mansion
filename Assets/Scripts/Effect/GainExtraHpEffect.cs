using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Effects/IntegerEffect/Gain Extra Hp Effect",
    fileName = "GainExtraHpEffect",
    order = 8)]
public class GainExtraHpEffect : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return $"Gain {value.ToString()} Extra Hp";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetExtraHp = target.extraHp;
        var finalExtraHp = targetExtraHp.Value + value;

        if (finalExtraHp > target.maxExtraHp)
        {
            Debug.Log("final extra hp is max now");
            finalExtraHp = target.maxExtraHp;
        }
        
        target.extraHp.setValue(finalExtraHp);
        Debug.Log("gain extra hp");
    }
}
