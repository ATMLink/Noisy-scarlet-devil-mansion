using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : ScriptableObject
{
    public List<Effect> effects = new List<Effect>();

    public abstract string GetName();
}
