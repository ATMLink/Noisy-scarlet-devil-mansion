using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionBase : MonoBehaviour
{
    protected Camera mainCamera;
    public LayerMask cardLayer;

    public CardDisplayManager cardDisplayManager;

    protected GameObject selectedCard;

    protected virtual void Awake()
    {
        mainCamera = Camera.main;
    }
}
