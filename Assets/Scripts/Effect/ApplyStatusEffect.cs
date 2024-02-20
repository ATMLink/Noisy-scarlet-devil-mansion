using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Apply Status Effect",
    fileName = "ApplyStatusEffect",
    order = 7)]
public class ApplyStatusEffect : IntegerEffect, IEntityEffect
{
    public StatusTemplate status;
    
    public override string getName()
    {
        if (status != null)
        {
            return $"Apply {value.ToString()} {status.Name}";
        }

        return "Apply status";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var currentValue = target.status.GetValue(status.Name);
        target.status.SetValue(status, currentValue + value);
    }
}
