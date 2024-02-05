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

    protected GameObject selectedCard;
    
    // protected Quaternion selectedRotation = Quaternion.Euler(0,0,0);//卡片竖直状态的角度;


    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }
}
