using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetableEffect : Effect
{
    public EffectTargetType target;

    public abstract void Resolve(RuntimeCharacter source, RuntimeCharacter target);
}
