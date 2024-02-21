using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathManager : BaseManager
{
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
        yield return new WaitForSeconds(0.2f);
        Debug.Log("game over");
    }
}
