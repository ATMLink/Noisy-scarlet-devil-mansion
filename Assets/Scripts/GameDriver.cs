using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class GameDriver : MonoBehaviour
{
    public CardBank startingDeck;

    private List<GameObject> enemies = new List<GameObject>();
    
    [Header("Manager")]
    [SerializeField] private CardsManager cardsManager;

    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private CardDeckManager cardDeckManager;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();

    [Header("Character pivots")] [SerializeField]
    public Transform enemyPivot;

    [SerializeField] private AssetReference enemyTemplate;
    
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

    private void createEnemy(AssetReference templateReference)
    {
        var handle = Addressables.LoadSceneAsync(templateReference);
        handle.Completed += operationResult =>
        {
            var pivot = enemyPivot;
            var template = operationResult.Result;
            // var enemy = Instantiate(template, pivot);
            
            // Assert.IsNotNull(enemy);
        };

    }

    public void initialize()
    {
        cardDeckManager.loadDeck(_playerDeck);
        cardDeckManager.shuffleDeck();
        cardDisplayManager.initialize(cardsManager);
        cardDeckManager.drawCardsFromDeck(5);
    }
}
