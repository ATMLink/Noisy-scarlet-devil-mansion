using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Events/Game event",
    fileName = "GameEvent",
    order = 0)]

public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

    public void raise()
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }
    
    public void registerListener(GameEventListener listener)
    {
        if(!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void unRegisterListener(GameEventListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
