using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "CardGame/Templates/Enemy",
    fileName = "Enemy",
    order = 2)]
public class EnemyTemplate : CharacterTemplate
{
    public List<Pattern> patterns = new List<Pattern>();
}
