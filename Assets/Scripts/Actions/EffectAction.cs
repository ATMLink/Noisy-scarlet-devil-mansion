using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectAction : ScriptableObject
{
    public abstract string getName();

    public abstract void execute(GameObject gameObj);
}
