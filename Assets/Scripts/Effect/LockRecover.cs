using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Effects/MechanismControl/Lock Recover",
    fileName = "LockRecover")]
public class LockRecover : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return $"Recover have locked {value.ToString()} turn";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        
    }
}
