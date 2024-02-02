using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    public CardBank startingDeck;
    [Header("Manager")]
    [SerializeField] private CardsManager cardsManager;

    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private CardDeckManager cardDeckManager;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();
    private void Start()
    {
        cardsManager.initialize();
        createPlayer();
        
    }
    private void createPlayer()
    {
        foreach (var item in startingDeck.Items)
        {
            for(int i = 0;i<item.amount;i++)
            {
                _playerDeck.Add(item.card);
            }
        }
        initialize();
    }
    public void initialize()
    {
        cardDeckManager.loadDeck(_playerDeck);
        cardDeckManager.shuffleDeck();
        cardDisplayManager.initialize(cardsManager);
        cardDeckManager.drawCardsFromDeck(10);
    }
}
