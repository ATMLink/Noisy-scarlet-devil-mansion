using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class GameDriver : MonoBehaviour
{
    public CardBank startingDeck;

    private GameObject player; 
    private List<GameObject> enemies = new List<GameObject>();
    
    [Header("Manager")]
    [SerializeField] private CardsManager cardsManager;

    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private CardDeckManager cardDeckManager;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();

    [Header("Character pivots")] [SerializeField]
    public Transform playerPivot;
    [SerializeField]
    public Transform enemyPivot;

    [SerializeField] private AssetReference _enemyTemplate;
    [SerializeField] private AssetReference _playerTemplate;
    
    private void Start()
    {
        cardsManager.initialize();
        createPlayer(_playerTemplate);
        createEnemy(_enemyTemplate);
    }
    private void createPlayer(AssetReference playerTemplateReference)
    {
        var handle = Addressables.LoadAssetAsync<RemiTemplate>(playerTemplateReference);
        handle.Completed += operationResult =>
        {
            var template = operationResult.Result;
            player = Instantiate(template.prefab, playerPivot);
            Assert.IsNotNull(player);
            
            foreach (var item in template.startingDeck.Items)
            {
                for(int i = 0;i<item.amount;i++)
                {
                    _playerDeck.Add(item.card);
                }
            }
            
            initialize();
            
        };
        
        
    }

    private void createEnemy(AssetReference templateReference)
    {
        var handle = Addressables.LoadAssetAsync<EnemyTemplate>(templateReference);
        handle.Completed += operationResult =>
        {
            var pivot = enemyPivot;
            var template = operationResult.Result;
            var enemy = Instantiate(template.prefab, pivot);
            
            Assert.IsNotNull(enemy);

            var obj = enemy.GetComponent<CharacterObject>();
            obj.characterTemplate = template;
            
            enemies.Add(enemy);

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
