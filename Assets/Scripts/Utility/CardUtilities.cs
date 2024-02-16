using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtilities
{
    public static bool cardHasTargetableEffect(CardTemplate card)
    {
        //for judge the card if need attack arrow by check the list of effects contain a targetable effect
        foreach (var effect in card.Effects)
        {
            if (effect is TargetableEffect targetableEffect)
            {
                if (targetableEffect.target == EffectTargetType.targetEnemy)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
