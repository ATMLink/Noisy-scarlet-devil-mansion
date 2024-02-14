using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "CardGame/Templates/Remi",
    fileName = "Remi",
    order = 1)]

public class RemiTemplate : CharacterTemplate
{
    public CardBank startingDeck;
    
}
