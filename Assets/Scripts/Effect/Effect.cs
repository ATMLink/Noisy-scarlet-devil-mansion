using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public List<EffectActionGroupManager> sourceAction = new List<EffectActionGroupManager>();
    public List<EffectActionGroupManager> targetAction = new List<EffectActionGroupManager>();
    
    public abstract string getName();
}
