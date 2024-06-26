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
        
        
    }

    public void OnEnemyTurnEnd()
    {
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

    public void OnEnemyMaxHPChanged(int value)
    {
        // 检查 player 和 character 对象
        if (player != null && player.character != null && player.character.status != null) 
        {
            // 检查状态模板 "Mihashira" 是否存在
            if (player.character.status.template.ContainsKey("Mihashira"))
            {
                var statusTemplate = player.character.status.template["Mihashira"];

                if (statusTemplate != null)
                {
                    var currentStatusValue = player.character.status.GetValue(statusTemplate.Name);

                    var newStatusValue = CalculateReducedValue(currentStatusValue, statusTemplate);

                    player.character.status.SetValue(statusTemplate, newStatusValue);
                }
            }
        }
    }
    
    private int CalculateReducedValue(int currentValue, StatusTemplate template)
    {
        return Mathf.Max(0, currentValue - 1);
    }
}
