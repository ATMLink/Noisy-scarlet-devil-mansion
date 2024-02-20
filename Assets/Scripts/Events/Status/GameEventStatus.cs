using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Events/Game event (Status)",
    fileName = "GameEventStatus",
    order = 2)]
public class GameEventStatus : ScriptableObject
{
    private List<GameEventStatusListener> _listeners = new List<GameEventStatusListener>();

    public void Raise(StatusTemplate status, int value)
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(status, value);
    }
    
    public void RegisterListener(GameEventStatusListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void UnregisterListener(GameEventStatusListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
