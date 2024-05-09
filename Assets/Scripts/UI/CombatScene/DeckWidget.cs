using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLabel;
    private int deckSize;

    public void SetAmount(int amount)
    {
        deckSize = amount;
        textLabel.text = amount.ToString();
    }

    public void RemoveCard()
    {
        SetAmount(deckSize - 1);
    }
}
