using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardGame/Templates/Card", order = 0)]
public class CardTemplate : ScriptableObject
{
    public int id;
    public string name;
    public int cost;
    public Sprite illustration;
    public CardType type;
    public string description;
    public List<Effect> Effects = new List<Effect>();
}
