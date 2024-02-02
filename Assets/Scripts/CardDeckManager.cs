using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    private List<RuntimeCard> _deck;
    private const int deckCapacity = 30;

    private void Awake()
    {
        _deck = new List<RuntimeCard>(deckCapacity);
    }

    public int loadDeck(List<CardTemplate> playerDeck)
    {
        var deckSize = 0;
        foreach(var template in playerDeck)
        {
            if(template ==null) continue;
            var card = new RuntimeCard
            {
                Template = template
            };

            _deck.Add(card);
            ++deckSize;
        }
        return deckSize;
    }
    public void shuffleDeck()
    {
        _deck.shuffle();
    }

    public void drawCardsFromDeck(int amount)
    {
        var deckSize = _deck.Count;
        if(deckSize >= amount)
        {
            var previousDeckSize = deckSize;
            var drawnCards = new List<RuntimeCard>(amount);
            for(var i = 0;i<amount; i++)
            {
                var card = _deck[0];
                _deck.RemoveAt(0);
                drawnCards.Add(card);
            }        
        }
    }
}
