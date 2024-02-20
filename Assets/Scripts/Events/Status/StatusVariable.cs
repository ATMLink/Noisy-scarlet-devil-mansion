using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;


[CreateAssetMenu(
    menuName = "CardGame/Variables/Status Variable",
    fileName = "StatusVariable",
    order = 1)]
public class StatusVariable : ScriptableObject
{
    public Dictionary<string, int> value = new Dictionary<string, int>();
    public Dictionary<string, StatusTemplate> template = new Dictionary<string, StatusTemplate>();

    public GameEventStatus valueChangedEvent;

    public int GetValue(string status)
    {
        if (value.ContainsKey(status))
        {
            return value[status];
        }

        return 0;
    }

    public void SetValue(StatusTemplate status, int value)
    {
        var statusName = status.Name;
        this.value[statusName] = value;
        valueChangedEvent?.Raise(status, value);

        if (!template.ContainsKey(statusName))
        {
            template.Add(statusName, status);
        }
    }
}
