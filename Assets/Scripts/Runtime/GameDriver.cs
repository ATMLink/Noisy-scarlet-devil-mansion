using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class GameDriver : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    
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
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private EnemyAIManager enemyAIManager;
    [SerializeField] private PlayerSpManager playerSpManager;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();

    [Header("Character pivots")] [SerializeField]
    public Transform playerPivot;
    [SerializeField]
    public Transform enemyPivot;

    [Header("UI")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _enemyHpWidget;
    [SerializeField] private GameObject _playerHpWidget;
    [SerializeField] private GameObject _enemyIntentWidget;
    [SerializeField] private IntVariable _playerShield;
    [SerializeField] private IntVariable _enemyShield;
    [SerializeField] private GameObject _playerStatusWidget;
    [SerializeField] private SpWidget _playerSpWidget;
 
    [Header("Variavles")]
    [SerializeField] private IntVariable _enemyHp;
    [SerializeField] private IntVariable _playerHp;
    [SerializeField] private StatusVariable _playerStatusVariable;
    
    [Header("Character Template")]
    [SerializeField] private AssetReference _enemyTemplate;
    [SerializeField] private AssetReference _playerTemplate;

    
    
    private void Start()
    {
        cardsManager.initialize();
        
        //set cursor texture
        setCursorTexture();
        
        createPlayer(_playerTemplate);
        createEnemy(_enemyTemplate);
        _mainCamera = Camera.main;
    }

    private void setCursorTexture()
    {
        float x, y;
        x = cursorTexture.width / 2.0f;
        y = cursorTexture.height / 2.0f;
        Cursor.SetCursor(cursorTexture, new Vector2(x,y), cursorMode);
    }
        
    private void createPlayer(AssetReference playerTemplateReference)
    {
        var handle = Addressables.LoadAssetAsync<RemiTemplate>(playerTemplateReference);
        handle.Completed += operationResult =>
        {
            var template = operationResult.Result;
            player = Instantiate(template.prefab, playerPivot);
            Assert.IsNotNull(player);

            _playerHp.Value = 20;
            _playerShield.Value = 0;
            playerSpManager.SetDefaultSp(3);
            
            createHpWidget(_playerHpWidget, player, _playerHp, 20, _playerShield);
            CreateStatusWidget(_playerStatusWidget, player);
            
            _playerSpWidget.Initialize(playerSpManager.playerSpVariable);
            
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
                shield = _playerShield,
                status = _playerStatusVariable,
                sp = 100,
                maxHp = 20
            };
            obj.character.status.value.Clear();
            
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
            _enemyShield.Value = 0;
            createHpWidget(_enemyHpWidget, enemy, _enemyHp,20, _enemyShield);
            CreateIntentWidget(_enemyIntentWidget, enemy);
            
            var obj = enemy.GetComponent<CharacterObject>();
            obj.characterTemplate = template;
            obj.character = new RuntimeCharacter()
            {
                hp = _enemyHp,
                shield = _enemyShield,
                sp = 100,
                maxHp = 20
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
        var enemyCharacters = new List<CharacterObject>(enemies.Count);

        foreach (var enemy in enemies)
        {
            enemyCharacters.Add(enemy.GetComponent<CharacterObject>());
        }

        cardSelectionWithArrow.Initialize(playerCharacter, enemyCharacters);
        enemyAIManager.Initialize(playerCharacter, enemyCharacters);
        effectResolutionManager.Initialize(playerCharacter, enemyCharacters);
        
        turnManager.BeginGame();
    }

    private void createHpWidget(GameObject prefab, GameObject character, IntVariable hp, int maxHp, IntVariable shield)
    {
        var hpWidget = Instantiate(prefab, _canvas.transform, false);
        var pivot = character.transform;
        var localScale = character.GetComponentInChildren<Transform>().localScale;
        var canvasPosition = _mainCamera.WorldToViewportPoint(pivot.position + 
                                                              new Vector3(0.0f, -2.0f, 0.0f));
        hpWidget.GetComponent<RectTransform>().anchorMin = canvasPosition;
        hpWidget.GetComponent<RectTransform>().anchorMax = canvasPosition;
        hpWidget.GetComponent<HpWidget>().Initialize(hp, maxHp, shield);
    }

    private void CreateIntentWidget(GameObject prefab, GameObject character)
    {
        var widget = Instantiate(prefab, _canvas.transform, false);
        var pivot = character.transform;
        var size = character.GetComponent<BoxCollider2D>().bounds.size;

        var canvasPosition = _mainCamera.WorldToViewportPoint(
            pivot.position + new Vector3(0.2f, size.y +0.7f, 0.0f)
            );
        widget.GetComponent<RectTransform>().anchorMin = canvasPosition;
        widget.GetComponent<RectTransform>().anchorMax = canvasPosition;
        
    }

    private void CreateStatusWidget(GameObject prefab, GameObject character)
    {
        var hpWidget = Instantiate(prefab, _canvas.transform, false);
        var pivot = character.transform;
        var canvasPosition = _mainCamera.WorldToViewportPoint(pivot.position + 
                                                              new Vector3(0.0f, -0.8f, 0.0f));
        hpWidget.GetComponent<RectTransform>().anchorMin = canvasPosition;
        hpWidget.GetComponent<RectTransform>().anchorMax = canvasPosition;
    }
}
