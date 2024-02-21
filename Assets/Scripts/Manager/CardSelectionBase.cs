using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌选择基类
/// </summary>
public class CardSelectionBase : BaseManager
{
    protected Camera mainCamera;
    public LayerMask cardLayer;

    public CardDisplayManager cardDisplayManager;
    public EffectResolutionManager effectResolutionManager;
    public CardDeckManager deckManager;

    protected GameObject selectedCard;
    public IntVariable playerSp;

    public LayerMask enemyLayer; 
    
    // protected Quaternion selectedRotation = Quaternion.Euler(0,0,0);//卡片竖直状态的角度;


    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void playSelectedCard()
    {
        var cardObject = selectedCard.GetComponent<CardObject>();
        var cardTemplate = cardObject.template;
        playerSp.setValue(playerSp.Value - cardTemplate.cost);
        
        cardDisplayManager.reorganizeHandCards(selectedCard);//reorganize hand cards
        cardDisplayManager.MoveCardToDiscardPile(selectedCard);
        
        deckManager.MoveCardToDiscardPile(cardObject.runtimeCard);
    }
}
