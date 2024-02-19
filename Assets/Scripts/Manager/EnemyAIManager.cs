using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// for administer enemy AI decision
/// </summary>
public class EnemyAIManager : BaseManager
{
    [SerializeField] private EffectResolutionManager effectResolutionManager;

    [SerializeField] private List<IntentChangeEvent> intentChangeEvents;

    private int _currentReplicateCount;

    private List<EnemyAI> _brains;

    private const float ThinkingTime = 1.5f;

    public override void Initialize(CharacterObject player, List<CharacterObject> enemies)
    {
        base.Initialize(player, enemies);
        _brains = new List<EnemyAI>(enemies.Count);

        foreach (var enemy in enemies)
        {
            _brains.Add(new EnemyAI(enemy));
        }
    }

    public void OnPlayerTurnBegan()
    {
        const int enemyIndex = 0;

        foreach (var enemy in enemies)
        {
            var template = enemy.characterTemplate as EnemyTemplate;
            var brain = _brains[enemyIndex];

            if (template != null)
            {
                if (brain.patternIndex >= template.patterns.Count)
                {
                    brain.patternIndex = 0;
                }

                Sprite sprite = null;
                var pattern = template.patterns[brain.patternIndex];

                if (pattern is ReplicatePattern replicatePattern)
                {
                    _currentReplicateCount += 1;
                    if (_currentReplicateCount == replicatePattern.times)
                    {
                        _currentReplicateCount = 0;
                        brain.patternIndex += 1;
                    }

                    brain.effects = pattern.effects;
                    sprite = replicatePattern.sprite;
                }
                else if (pattern is RandomPattern randomPattern)
                {
                    var effects = new List<int>();
                    var index = 0;
                    
                    // based on set probability, generate corresponding effect quantity list, then randomly select
                    foreach (var probability in randomPattern.probabilities)
                    {
                        var amount = probability.value;
                        for (var i = 0; i < amount; i++)
                        {
                            effects.Add(index);
                        }

                        index += 1;
                    }

                    var randomIndex = Random.Range(0, effects.Count - 1);
                    var selectedEffect = randomPattern.effects[effects[randomIndex]];
                    brain.effects = new List<Effect>{selectedEffect};

                    for (var i = 0; i < pattern.effects.Count; i++)
                    {
                        var effect = pattern.effects[i];

                        if (effect == selectedEffect)
                        {
                            sprite = randomPattern.probabilities[i].sprite;
                            break;
                        }
                    }
                    
                    brain.patternIndex += 1;
                }

                var currentEffect = brain.effects[0];
                if (currentEffect)
                {
                    intentChangeEvents[enemyIndex].Raise(sprite, (currentEffect as IntegerEffect).value);
                }
            }
        }
    }

    public void OnEnemyTurnBegan()
    {
        StartCoroutine(ProcessEnemyBrains());
    }

    private IEnumerator ProcessEnemyBrains()
    {
        foreach (var brain in _brains)
        {
            effectResolutionManager.SetCurrentEnemy(brain.enemy);
            effectResolutionManager.ResolveEnemyEffects(brain.enemy, brain.effects);
            yield return new WaitForSeconds(ThinkingTime);
        }
    }
}
