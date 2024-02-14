using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectResolutionManager : BaseManager
{
    private CharacterObject _currentEnemy;

    //analyse effect
    public void ResolveCardEffect(RuntimeCard card, CharacterObject playerSelectedTarget)
    {
        foreach (var effect in card.Template.Effects)//ergodic effect in this card
        {
            var targetableEffect = effect as TargetableEffect;

            if (targetableEffect != null)
            {
                var targets = getTargets(targetableEffect, playerSelectedTarget, true);
            }
        }
    }

    private List<CharacterObject> getTargets(TargetableEffect effect, 
        CharacterObject playerSelectedTarget,
        bool playerSource)
    {
        var targets = new List<CharacterObject>(4);
        //player is action initiator
        if (playerSource)
        {
            switch (effect.target)
            {
                case EffectTargetType.Self :
                    targets.Add(player);//player will be valued in the class named GameDriver
                    break;
                case EffectTargetType.targetEnemy:
                    targets.Add(playerSelectedTarget);
                    break;
            }
        }
        //enemy is action initiator
        else
        {
            switch (effect.target)
            {
                case EffectTargetType.Self:
                    targets.Add(_currentEnemy);
                    break;
                case EffectTargetType.targetEnemy :
                    targets.Add(player);
                    break;
            }
        }

        return targets;
    }
}
