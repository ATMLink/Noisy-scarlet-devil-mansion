using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// turn manager
/// based by time system event, when game begin it is player turn, when turn over button down it is enemy turn
/// after enemy acted enemy turn end, then change to player turn.
/// </summary>
public class TurnManager : MonoBehaviour
{
    public GameEvent playerTurnBegan;
    public GameEvent playerTurnEnded;
    public GameEvent enemyTurnBegan;
    public GameEvent enemyTurnEnded;

    private bool _isEnemyTurn;
    private float _timer;
    private bool _isEndOfGame;

    private const float EnemyTurnDuration = 3.0f;

    private void Update()
    {
        if (_isEnemyTurn)
        {
            _timer += Time.deltaTime;

            if (_timer >= EnemyTurnDuration)
            {
                _timer = 0.0f;
                EndEnemyTurn();
                BeginPlayerTurn();
            }
        }
    }

    public void BeginGame()
    {
        BeginPlayerTurn();
    }

    public void BeginPlayerTurn()
    {
        playerTurnBegan.raise();    
        Debug.Log("player turn began");
    }

    public void EndPlayerTurn()
    {
        playerTurnEnded.raise();
        BeginEnemyTurn();
        Debug.Log("player turn ended");
    }
    
    public void BeginEnemyTurn()
    {
        enemyTurnBegan.raise();
        _isEnemyTurn = true;
        Debug.Log("Enemy turn began");
    }
    
    public void EndEnemyTurn()
    {
        enemyTurnEnded.raise();
        _isEnemyTurn = false;
        Debug.Log("Enemy turn ended");
    }

    public void SetEndOfGame(bool value)
    {
        _isEndOfGame = value;
        Debug.Log("End of Game");
    }

    public bool IsEndOfGame()
    {
        return _isEndOfGame;
    }
}
