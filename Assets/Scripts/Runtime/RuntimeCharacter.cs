using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeCharacter
{
    public IntVariable hp;
    public IntVariable shield;
    public int sp;
    public int life;
    public int extraHp;
    public int maxHp;
    public int maxSp;
    public int maxExtraHp;
    public const int maxLife = 3;
    public StatusVariable status;
}
