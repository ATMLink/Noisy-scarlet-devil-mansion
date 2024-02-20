using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    menuName = "CardGame/Templates/Status",
    fileName = "Status",
    order = 6)]
public class StatusTemplate : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
}
