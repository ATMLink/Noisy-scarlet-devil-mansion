using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private int maxHp;

    private float _timer = 0.0f;
    
    [Header("Managers")]
    [SerializeField] private CardsManager cardsManager;
    [SerializeField] private CardDisplayManager cardDisplayManager;
    [SerializeField] private CardDeckManager cardDeckManager;
    [SerializeField] private EffectResolutionManager effectResolutionManager;
    [SerializeField] private CardSelectionWithArrow cardSelectionWithArrow;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private EnemyAIManager enemyAIManager;
    [SerializeField] private PlayerSpManager playerSpManager;
    [SerializeField] private CharacterDeathManager characterDeathManager;
    [SerializeField] private RealTimeRecoverManager realTimeRecoverManager;

    private List<CardTemplate> _playerDeck = new List<CardTemplate>();

    [Header("Character pivots")] [SerializeField]
    public Transform playerPivot;
    [SerializeField]
    public List<Transform> enemyPivots;

    [Header("UI")]
    [SerializeField] private List<GameObject> _enemyHpWidget;
    [SerializeField] private List<GameObject> _enemyExtraHpWidget;
    [SerializeField] private List<GameObject> _enemyIntentWidget;
    [SerializeField] private IntVariable _enemyShield;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _playerHpWidget;
    [SerializeField] private IntVariable _playerShield;
    [SerializeField] private GameObject _playerStatusWidget;
    [SerializeField] private SpWidget _playerSpWidget;
    [SerializeField] private DeckWidget _deckWidget;
    [SerializeField] private DiscardPileWidget _discardPileWidget;
 
    [Header("Variables")]
    [SerializeField] private List<IntVariable> _enemyHPs;
    [SerializeField] private List<IntVariable> _enemyExtraHPs;
    [SerializeField] private List<IntVariable> _enemyMaxHPs;
    [SerializeField] private List<IntVariable> _enemyMaxExtraHPs;
    [SerializeField] private IntVariable _playerHp;
    [SerializeField] private StatusVariable _playerStatusVariable;
    [SerializeField] private IntVariable _playerMaxHp;
    [SerializeField] private IntVariable _overDamage;
    [SerializeField] private IntVariable _recoverHpCounter;
    
    [Header("Character Template")]
    [SerializeField] private List<AssetReference> _enemyTemplates;
    [SerializeField] private AssetReference _playerTemplate;

    
    private void Start()
    {
        cardsManager.initialize();
        
        //set cursor texture
        setCursorTexture();
        
        createPlayer(_playerTemplate);
        for (int i = 0; i < _enemyTemplates.Count; i++)
        {
            createEnemy(_enemyTemplates[i], _enemyHpWidget[i],_enemyExtraHpWidget[i], enemyPivots[i],
                _enemyHPs[i], _enemyMaxHPs[i], _enemyExtraHPs[i], _enemyMaxExtraHPs[i],
                _enemyIntentWidget[i]);
        }
        
        _mainCamera = Camera.main;
        _recoverHpCounter.setValue(0);
    }

    private void Update()
    {
        // realTimeRecoverManager.RecoverControl(true);
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

            _playerHp.setValue(_playerMaxHp.Value);
            _playerShield.Value = 0;
            playerSpManager.SetDefaultSp(3);
            createHpWidget(_playerHpWidget, player, _playerHp, _playerMaxHp.Value, _playerShield);
            CreateStatusWidget(_playerStatusWidget, player);
            
            _playerSpWidget.Initialize(playerSpManager.playerSpVariable, playerSpManager.GetMaxSp());
            
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
                maxHp = _playerMaxHp.Value
            };
            obj.character.status.value.Clear();
            
            initialize();
            
        };
        
        
    }

    private void createEnemy(AssetReference templateReference, GameObject enemyHPWidget, GameObject enemyExtraHPWidget, 
        Transform enemyPivot, IntVariable enemyHP, IntVariable enemyMaxHP, IntVariable enemyExtraHP,
        IntVariable enemyMaxExtraHP, GameObject enemyIntentWidget)
    {
        var handle = Addressables.LoadAssetAsync<EnemyTemplate>(templateReference);
        handle.Completed += operationResult =>
        {
            var pivot = enemyPivot;
            var template = operationResult.Result;
            var enemy = Instantiate(template.prefab, pivot);
            
            Assert.IsNotNull(enemy);

            enemyHP.setValue(enemyMaxHP.Value);
            enemyExtraHP.setValue(enemyMaxExtraHP.Value);
            _enemyShield.Value = 0;
            createHpWidget(enemyHPWidget, enemy, enemyHP,enemyHP.Value, _enemyShield);
            createHpWidget(enemyExtraHPWidget, enemy, enemyExtraHP, enemyExtraHP.Value, _enemyShield, 0.3f);
            CreateIntentWidget(enemyIntentWidget, enemy);
            
            var obj = enemy.GetComponent<CharacterObject>();
            obj.characterTemplate = template;
            obj.character = new RuntimeCharacter()
            {
                hp = enemyHP,
                shield = _enemyShield,
                extraHp = enemyExtraHP,
                maxHp = enemyMaxHP.Value,
                maxExtraHp = enemyMaxExtraHP.Value
            };
            
            enemies.Add(enemy);

        };

    }

    public void initialize()
    {
        cardDeckManager.Initialize(_deckWidget, _discardPileWidget);
        cardDeckManager.loadDeck(_playerDeck);
        cardDeckManager.shuffleDeck();
        cardDisplayManager.initialize(cardsManager, _deckWidget, _discardPileWidget);

        Assert.IsNotNull(enemies);
        Assert.IsNotNull(player);
        
        var playerCharacter = player.GetComponent<CharacterObject>();
        var enemyCharacters = new List<CharacterObject>(enemies.Count);

        foreach (var enemy in enemies)
        {
            Debug.Log("enemy has been created");
            enemyCharacters.Add(enemy.GetComponent<CharacterObject>());
        }

        cardSelectionWithArrow.Initialize(playerCharacter, enemyCharacters);
        enemyAIManager.Initialize(playerCharacter, enemyCharacters);
        effectResolutionManager.Initialize(playerCharacter, enemyCharacters);
        characterDeathManager.Initialize(playerCharacter, enemyCharacters);
        // enemyCharacters with every enemy
        
        turnManager.BeginGame();
    }

    private void createHpWidget(GameObject prefab, GameObject character, IntVariable hp, int maxHp, IntVariable shield, float offset = 0.0f)
    {
        var hpWidget = Instantiate(prefab, _canvas.transform, false);
        var pivot = character.transform;
        var localScale = character.GetComponentInChildren<Transform>().localScale;
        var canvasPosition = _mainCamera.WorldToViewportPoint(pivot.position + 
                                                              new Vector3(0.0f, -(2.0f+offset), 0.0f));
        hpWidget.GetComponent<RectTransform>().anchorMin = canvasPosition;
        hpWidget.GetComponent<RectTransform>().anchorMax = canvasPosition;
        hpWidget.GetComponent<HpWidget>().Initialize(hp, maxHp, shield);
    }

    // private void createEnemyHpWidget(GameObject prefab, GameObject character, IntVariable value, int maximum, float offset = 0)
    // {
    //     var enemyHpWidget = Instantiate(prefab, _canvas.transform, false);
    //     var pivot = character.transform;
    //     var canvasPosition = _mainCamera.WorldToViewportPoint(pivot.position + new Vector3(0.0f, -(2.0f+offset), 0.0f));
    //     enemyHpWidget.GetComponent<RectTransform>().anchorMin = canvasPosition;
    //     enemyHpWidget.GetComponent<RectTransform>().anchorMax = canvasPosition;
    //     enemyHpWidget.GetComponent<HpWidget>().Initialize(value, maximum,);
    // }

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
