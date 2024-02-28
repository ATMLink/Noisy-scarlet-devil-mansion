using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeCharacter
{
    public IntVariable hp;
    public IntVariable shield;
    public IntVariable extraHp;
    public int life;
    public int maxHp;
    public int maxSp;
    public int maxExtraHp;
    public const int maxLife = 3;
    public StatusVariable status;
}
