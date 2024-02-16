using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effect/IntegerEffect",
    fileName = "IntegerEffect",
    order = 4)]
public class DealDamageEffect : IntegerEffect, IEntityEffect
{
    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetHp = target.hp;
        var hp = targetHp.value;
        
        var damage = value;
        Debug.Log("deal damage"+damage);

        var newHp = hp - damage;
        if (newHp < 0)
        {
            newHp = 0;
        }
        
        targetHp.setValue(newHp);
    }
    public override string getName()
    {
        return $"Deal {value.ToString()} damage";
    }
}
