using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathManager : BaseManager
{
    [SerializeField] private float endBattlePopupDelay = 1.0f;
    [SerializeField] private EndBattlePopup endBattlePopup;
    
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
