using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityEffect
{
    void Resolve(RuntimeCharacter source, RuntimeCharacter target);
}
