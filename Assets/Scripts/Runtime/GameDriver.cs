using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class GameDriver : MonoBehaviour
{
    private Camera _mainCamera;
    public CardBank startingDeck;

    private GameObject player; 
    private List<GameObject> enemies = new List<GameObject>();
    
    [Header("Managers")]
    [SerializeField] private CardsManager cardsManager;

    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private CardDeckManager cardDeckManager;
    [SerializeField] private EffectResolutionManager effectResolutionManager;
    [SerializeField] private CardSelectionWithArrow cardSelectionWithArrow;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();

    [Header("Character pivots")] [SerializeField]
    public Transform playerPivot;
    [SerializeField]
    public Transform enemyPivot;

    [Header("UI")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject hpWidget;

    [Header("Variavles")]
    [SerializeField] private IntVariable _enemyHp;
    [SerializeField] private IntVariable _playerHp;
    
    [Header("Character Template")]
    [SerializeField] private AssetReference _enemyTemplate;
    [SerializeField] private AssetReference _playerTemplate;

    
    
    private void Start()
    {
        cardsManager.initialize();
        createPlayer(_playerTemplate);
        createEnemy(_enemyTemplate);
        _mainCamera = Camera.main;
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

            var obj = player.GetComponent<CharacterObject>();
            obj.characterTemplate = template;
            obj.character = new RuntimeCharacter()
            {
                hp = _playerHp,
                shield = 100,
                sp = 100,
                maxHp = 100
            };
            
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

            _enemyHp.Value = 20;
            createHpWidget(hpWidget, enemy, _enemyHp,20);
            
            var obj = enemy.GetComponent<CharacterObject>();
            obj.characterTemplate = template;
            obj.character = new RuntimeCharacter()
            {
                hp = _enemyHp,
                shield = 100,
                sp = 100,
                maxHp = 100
            };
            
            enemies.Add(enemy);

        };

    }

    public void initialize()
    {
        cardDeckManager.loadDeck(_playerDeck);
        cardDeckManager.shuffleDeck();
        cardDisplayManager.initialize(cardsManager);
        cardDeckManager.drawCardsFromDeck(5);

        var playerCharacter = player.GetComponent<CharacterObject>();
        var enemyCharacter = new List<CharacterObject>(enemies.Count);

        foreach (var enemy in enemies)
        {
            enemyCharacter.Add(enemy.GetComponent<CharacterObject>());
        }

        cardSelectionWithArrow.Initialize(playerCharacter, enemyCharacter);
        effectResolutionManager.Initialize(playerCharacter, enemyCharacter);
    }

    private void createHpWidget(GameObject prefab, GameObject character, IntVariable hp, int maxHp)
    {
        var hpWidget = Instantiate(prefab, _canvas.transform, false);
        var pivot = character.transform;
        var localScale = character.GetComponentInChildren<Transform>().localScale;
        var canvasPosition = _mainCamera.WorldToViewportPoint(pivot.position + 
                                                              new Vector3(0.0f, -(localScale.y*2), 0.0f));
        hpWidget.GetComponent<RectTransform>().anchorMin = canvasPosition;
        hpWidget.GetComponent<RectTransform>().anchorMax = canvasPosition;
        hpWidget.GetComponent<HpWidget>().Initialize(hp, maxHp);
    }
}
