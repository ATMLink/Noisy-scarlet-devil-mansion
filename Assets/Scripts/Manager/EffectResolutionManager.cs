using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectResolutionManager : BaseManager
{
    public CardSelectionWithArrow _cardSelectionWithArrow;
    
    private CharacterObject _currentEnemy;

    //analyse effect with target (resolve attack card effect)
    public void ResolveCardEffect(RuntimeCard card, CharacterObject playerSelectedTarget)
    {
        foreach (var effect in card.Template.Effects)//ergodic effect in this card
        {
            var targetableEffect = effect as TargetableEffect;

            if (targetableEffect != null)
            {
                var targets = getTargets(targetableEffect, playerSelectedTarget, true);
                foreach (var target in targets)
                {
                    targetableEffect.Resolve(player.character, target.character);

                    foreach (var groupManager in targetableEffect.sourceAction)
                    {
                        foreach (var action in groupManager.group.actions)
                        {
                            action.execute(player.gameObject);
                        }
                    }
                    
                    foreach (var groupManager in targetableEffect.targetAction)
                    {
                        foreach (var action in groupManager.group.actions)
                        {
                            var enemy = _cardSelectionWithArrow.getSelectedEnemy();
                            action.execute(enemy.gameObject);
                        }
                    }
                }
            }
        }
    }
    
    // analyse effect without target (resolve non-attack card effect)
    public void ResolveCardEffect(RuntimeCard card)
    {
        foreach (var effect in card.Template.Effects) //ergodic effect in this card
        {
            var targetableEffect = effect as TargetableEffect;

            if (targetableEffect != null)
            {
                var targets = getTargets(targetableEffect, null, true);

                foreach (var target in targets)
                {
                    targetableEffect.Resolve(player.character, target.character);
                }
            }
        }
    }

    public void SetCurrentEnemy(CharacterObject enemy)
    {
        _currentEnemy = enemy;
    }

    public void ResolveEnemyEffects(CharacterObject enemy, List<Effect> effects)
    {
        foreach (var effect in effects)
        {
            var targetableEffect = effect as TargetableEffect;
            if (targetableEffect != null)
            {
                var targets = getTargets(targetableEffect, null, false);

                foreach (var target in targets)
                {
                    targetableEffect.Resolve(enemy.character, target.character);
                }
                
                foreach (var groupManager in targetableEffect.sourceAction)
                {
                    foreach (var action in groupManager.group.actions)
                    {
                        action.execute(enemy.gameObject);
                    }
                }
                
                foreach (var groupManager in targetableEffect.targetAction)
                {
                    foreach (var action in groupManager.group.actions)
                    {
                        action.execute(player.gameObject);
                    }
                }
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
