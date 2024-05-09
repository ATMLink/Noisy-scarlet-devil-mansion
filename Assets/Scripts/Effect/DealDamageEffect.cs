using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Effects/IntegerEffect/Deal Damage Effect",
    fileName = "DealDamageEffect",
    order = 4)]
public class DealDamageEffect : IntegerEffect, IEntityEffect
{
    
    [SerializeField] private IntVariable overDamage;
    [SerializeField] private IntVariable combo; 
    public override void Resolve(RuntimeCharacter source, RuntimeCharacter target)
    {
        
        var targetHp = target.hp;
        var hp = targetHp.Value;

        var targetShield = target.shield;
        var shield = targetShield.Value;
        var comboIncreasing = (int) (combo.Value * 0.2 * value);
        var damage = value + comboIncreasing;
        
        if (source.status != null)
        {
            var currentWeakValue = source.status.GetValue("Weak");
            if (currentWeakValue > 0)
            {
                damage = (int)Mathf.Floor(damage * 0.75f);
            }
            
            var currentStrengthValue = source.status.GetValue("Strength");

            if (currentStrengthValue > 0)
            {
                damage = (int)Mathf.Floor(damage * 1.25f);
            }
        }

        if (damage >= shield)
        {
            var newHp = hp - (damage - shield);
            if (newHp < 0)
            {
                overDamage.setValue(damage - shield - hp);
                newHp = 0;
            }
            targetHp.setValue(newHp);
            targetShield.setValue(0);
        }
        else
        {
            targetShield.setValue(shield - damage);
        }
            
        combo.setValue(combo.Value + 1);
        
        Debug.Log("deal damage"+damage);

        // var newHp = hp - damage;
        // if (newHp < 0)
        // {
        //     newHp = 0;
        // }
        
        // targetHp.setValue(newHp);
    }
    public override string getName()
    {
        return $"Deal {value.ToString()} damage";
    }
}
