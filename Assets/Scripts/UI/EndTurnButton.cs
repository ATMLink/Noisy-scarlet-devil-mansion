using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button _button;

    private CardDisplayManager _cardDisplayManager;
    private CardSelectionWithArrow _cardSelectionWithArrow;
    private CardSelectionWithoutArrow _cardSelectionWithoutArrow;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _cardDisplayManager = FindFirstObjectByType<CardDisplayManager>();
        _cardSelectionWithArrow = FindFirstObjectByType<CardSelectionWithArrow>();
        _cardSelectionWithoutArrow = FindFirstObjectByType<CardSelectionWithoutArrow>();
    }

    public void OnButtonPressed()
    {
        if (_cardDisplayManager.getIsCardMoving())
        {
            return;
        }

        if (_cardSelectionWithArrow.HasSelectedCard()||
            _cardSelectionWithoutArrow.HasSelectedCard())
        {
            return;
        }

        _button.interactable = false;

        var turnManager = FindFirstObjectByType<TurnManager>();
        turnManager.EndPlayerTurn();
    }

    public void OnPlayerTurnBegan()
    {
        _button.interactable = true;
    }
}
