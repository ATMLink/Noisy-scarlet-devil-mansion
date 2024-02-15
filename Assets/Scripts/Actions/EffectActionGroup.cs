using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Actions/Action group",
    fileName = "Action group",
    order = 6)]
public class EffectActionGroup : ScriptableObject
{
    public List<EffectAction> actions = new List<EffectAction>();
    
}
