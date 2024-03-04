using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Deal Extra Damage Effect",
    fileName = "Deal Extra Damage Effect",
    order = 9)]

public class DealExtraDamageEffect : IntegerEffect, IEntityEffect
{
    public override string getName()
    {
        return $"Deal {value.ToString()} extra damage";
    }

    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        var targetExtraHp = target.extraHp;
        var extraHp = targetExtraHp.Value;

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
            var newExtraHp = extraHp - (damage - shield);
            var newHp = hp - (damage - shield - extraHp);
            if (newExtraHp < 0)
            {
                newExtraHp = 0;
                if (newHp < 0)
                {
                    newHp = 0;
                }
                targetHp.setValue(newHp);
            }
            targetExtraHp.setValue(newExtraHp);
            targetShield.setValue(0);
            
            // if extra hp is empty then make damage to hp.
        }
        else
        {
            targetShield.setValue(shield - damage);
        }
        
        Debug.Log("deal extra damage"+damage);
    }
}
