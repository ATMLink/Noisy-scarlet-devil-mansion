using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathManager : BaseManager
{
    [SerializeField] private float endBattlePopupDelay = 1.0f;
    [SerializeField] private EndBattlePopup endBattlePopup;
    [SerializeField] private List<IntVariable> enemyHPs;
    
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
            EndGame(false);
        }
        
        // var counter = 0;
        // foreach (var enemy in enemies)
        // {
        //     if (enemy.GetComponent<CharacterObject>().character.hp.Value <= 0)
        //     {
        //         enemy.OnCharacterDead();
        //         counter++;
        //     }
        // }
        // if (counter == enemies.Count)
        // {
        //     EndGame(false);
        // }
        
        
        // var counter = 0;
        // for (int i = 0; i < enemyHPs.Count; i++)
        // {
        //     if (enemyHPs[i].Value <= 0)
        //     {
        //         enemies[i].OnCharacterDead();
        //         counter++;
        //     }
        //     if(counter == enemies.Count)
        //         EndGame(false);
        // }


        
    }

    public void EndGame(bool characterDead)
    {
        StartCoroutine(ShowEndBattlePopup(characterDead));// start a new coroutine
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
