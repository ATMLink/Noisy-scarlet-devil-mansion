using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Variables/Integer",
    fileName = "IntegerVariable",
    order = 0)]
public class IntVariable : ScriptableObject
{
    public int value;

    public GameEventInt valueChangedEvent;

    public void setValue(int value)
    {
        this.value = value;
        Debug.Log("hp = "+value);
        valueChangedEvent?.raise(value);//if valueChangeEvent is not null execute raise()
    }
}
