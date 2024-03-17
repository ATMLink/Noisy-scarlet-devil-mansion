using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Deal Enemy Damage Effect",
    fileName = "DealEnemyDamage",
    order = 10)]

public class DealEnemyDamageEffect : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return "deal enemy damage";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetHp = target.hp;
        var hp = targetHp.Value;

        var targetShield = target.shield;
        var shield = targetShield.Value;

        var damage = value;
        
        if (source.status != null)
        {
            var weak = source.status.GetValue("Weak");
            if (weak > 0)
            {
                damage = (int)Mathf.Floor(damage * 0.75f);
            }
            
            var strength = source.status.GetValue("Strength");

            if (strength > 0)
            {
                damage = (int)Mathf.Floor(damage * 1.25f);
            }
        }

        if (damage >= shield)
        {
            var newHp = hp - (damage - shield);
            if (newHp < 0)
            {
                newHp = 0;
            }
            targetHp.setValue(newHp);
            targetShield.setValue(0);
        }
        else
        {
            targetShield.setValue(shield - damage);
        }
            
        Debug.Log(getName()+damage);
    }
}
