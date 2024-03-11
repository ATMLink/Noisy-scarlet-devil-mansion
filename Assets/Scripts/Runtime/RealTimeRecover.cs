using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeRecover : MonoBehaviour
{
    /// <summary>
    /// to control if recover 
    /// </summary>
    /// <param name="enemies"></param>
    public void Recover(List<CharacterObject> enemies, float timer)
    {
        foreach (var enemy in enemies)
        {
            var currentHp = enemy.character.hp.Value;
            timer += Time.deltaTime;
            if (timer % 1.5f != 0)
                return;
            currentHp++;
            // can not exceed max hp
            if (currentHp >= enemy.character.maxHp)
            {
                currentHp = enemy.character.maxHp;
            }
            enemy.character.hp.setValue(currentHp);
        }
    }
}
