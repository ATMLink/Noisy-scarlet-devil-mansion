using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
[CreateAssetMenu(menuName = "CardGame/Templates/Card Bank", fileName = "CardBank", order = 3)]

public class CardBank : ScriptableObject
{
    public string name;
    public List<CardBankItem> Items = new List<CardBankItem>();
}
