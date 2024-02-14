using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌选择基类
/// </summary>
public class CardSelectionBase : MonoBehaviour
{
    protected Camera mainCamera;
    public LayerMask cardLayer;

    public CardDisplayManager cardDisplayManager;
    public EffectResolutionManager effectResolutionManager;

    protected GameObject selectedCard;

    public LayerMask enemyLayer; 
    
    // protected Quaternion selectedRotation = Quaternion.Euler(0,0,0);//卡片竖直状态的角度;


    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void playSelectedCard()
    {
        cardDisplayManager.reorganizeHandCards(selectedCard);//reorganize hand cards
    }
}
