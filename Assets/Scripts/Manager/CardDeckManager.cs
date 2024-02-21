using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    private List<RuntimeCard> _deck;
    private List<RuntimeCard> _discardPile;
    private List<RuntimeCard> _hand;
    
    private const int deckCapacity = 30;
    private const int HandCapacity = 30;
    private const int DiscardPileCapacity = 30;
    
    public CardDisplayManager cardDisplayManager;

    private DeckWidget _deckWidget;
    private DiscardPileWidget _discardPileWidget;
    
    private void Awake()
    {
        _deck = new List<RuntimeCard>(deckCapacity);
        _discardPile = new List<RuntimeCard>(DiscardPileCapacity);
        _hand = new List<RuntimeCard>(HandCapacity);
    }

    public void Initialize(DeckWidget deckWidget, DiscardPileWidget discardPileWidget)
    {
        _deckWidget = deckWidget;
        _discardPileWidget = discardPileWidget;
    }
    
    public int loadDeck(List<CardTemplate> playerDeck)
    {
        var deckSize = 0;
        foreach(var template in playerDeck)
        {
            if(template ==null) 
                continue;
            var card = new RuntimeCard
            {
                Template = template
            };

            _deck.Add(card);
            ++deckSize;
        }
        
        _deckWidget.SetAmount(_deck.Count);
        _discardPileWidget.SetAmount(0);
        
        return deckSize;
    }
    /// <summary>
    /// 洗牌
    /// </summary>
    public void shuffleDeck()
    {
        _deck.shuffle();
    }

    /// <summary>
    /// 抽牌
    /// </summary>
    /// <param name="amount"></param>
    public void drawCardsFromDeck(int amount)
    {
        var deckSize = _deck.Count;
        if(deckSize >= amount)
        {
            var previousDeckSize = deckSize;
            var drawnCards = new List<RuntimeCard>(amount);
            for(var i = 0; i<amount; i++)
            {
                var card = _deck[0];
                _deck.RemoveAt(0);
                _hand.Add(card);
                drawnCards.Add(card);
            }
            cardDisplayManager.createHandCards(drawnCards, previousDeckSize);       
        }
        // if cards in deck not enough then shuffle discard pile and put them to deck, draw card to hand from deck.
        else
        {
            for (var i = 0; i < _discardPile.Count; i++)
            {
                _deck.Add(_discardPile[i]);
            }
            
            _discardPile.Clear();
            cardDisplayManager.UpdateDiscardPileSize(_discardPile.Count);
            
            // the condition of draw card amount exceed total cards in deck
            if (amount > _deck.Count + _discardPile.Count)
            {
                amount = _deck.Count + _discardPile.Count;
            }
            
            drawCardsFromDeck(amount);
        }
        
    }

    // execute when hand card used
    public void MoveCardToDiscardPile(RuntimeCard card)
    {
        _hand.Remove(card);
        _discardPile.Add(card);
    }
    
    // execute when player turn end
    public void MoveCardsToDiscardPile()
    {
        foreach (var card in _hand)
        {
            _discardPile.Add(card);
        }
        _hand.Clear();
    }
}
