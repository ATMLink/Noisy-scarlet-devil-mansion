using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTurnBaseManager : BaseManager
{
    public void OnPlayerTurnEnd()
    {
        // reduce player status value
        var playerStatusKeys = new List<string>(player.character.status.value.Keys);
        foreach (var statusName in playerStatusKeys)
        {
            if (player.character.status.template.ContainsKey(statusName))
            {
                var statusTemplate = player.character.status.template[statusName];
                var currentStatusValue = player.character.status.GetValue(statusTemplate.Name);
                var newStatusValue = CalculateReducedValue(currentStatusValue, statusTemplate);
                player.character.status.SetValue(statusTemplate, newStatusValue);
            }
            
        }
        
        // reduce enemies status value
        foreach (var enemy in enemies)
        {
            var enemyStatusKeys = new List<string>(enemy.character.status.value.Keys);
            foreach (var statusName in enemyStatusKeys)
            {
                if(enemy.character.status.template.ContainsKey(statusName))
                {
                    var statusTemplate = enemy.character.status.template[statusName];
                    var currentStatusValue =
                        enemy.character.status.GetValue(statusTemplate.Name);
                    var newStatusValue = CalculateReducedValue(currentStatusValue, statusTemplate);
                    enemy.character.status.SetValue(statusTemplate, newStatusValue);
                }
            }
        }
    }

    private int CalculateReducedValue(int currentValue, StatusTemplate template)
    {
        return Mathf.Max(0, currentValue - 1);
    }
}
