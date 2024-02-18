using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
    public CharacterObject enemy;

    public int patternIndex;

    public List<Effect> effects;

    public EnemyAI(CharacterObject enemy)
    {
        this.enemy = enemy;
    }
}
