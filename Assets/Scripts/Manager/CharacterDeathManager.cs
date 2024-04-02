using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CharacterDeathManager : BaseManager
{
    [SerializeField] private float endBattlePopupDelay = 1.0f;
    [SerializeField] private EndBattlePopup endBattlePopup;
    [SerializeField] private List<IntVariable> enemyHPs;

    private int counter = 0;
    
    public void OnPlayerHpChanged(int hp)
    {
        if (hp <= 0)
        {
            EndGame(true);
        }
    }

    
    public void OnEnemyHpChanged(int hp)
    {
        if (hp <= 0)
        {
            enemies[0].OnCharacterDead();
            counter++;
            // EndGame(false);
            if(counter == enemies.Count)
                EndGame(false);
        }
        //return;
    }

    public void OnEnemyHpChanged1(int hp)
    {
        if (hp > 0)
            return;
        enemies[0].OnCharacterDead();
        counter++;
        // EndGame(false);
        if(counter == enemies.Count)
            EndGame(false);
    }
    public void OnEnemyTwoHPChanged(int hp)
    {
        if (hp > 0)
            return;
        enemies[1].OnCharacterDead();
        counter++;
        if (counter == enemies.Count)
            EndGame(false);
    }
    public void EndGame(bool characterDead)
    {
        StartCoroutine(ShowEndBattlePopup(characterDead));// start a new coroutine
        counter = 0;// reset counter
    }

    private IEnumerator ShowEndBattlePopup(bool characterDead)
    {
        yield return new WaitForSeconds(endBattlePopupDelay);

        if (endBattlePopup != null)
        {
            endBattlePopup.Show();

            if (characterDead)
            {
                endBattlePopup.SetDefeatText();
            }
            else
            {
                endBattlePopup.SetVictoryText();
            }

            var turnManagement = FindFirstObjectByType<TurnManager>();
            turnManagement.SetEndOfGame(true);
        }
    }
}
