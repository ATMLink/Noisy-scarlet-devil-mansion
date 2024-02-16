using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Events/Game event (int)",
    fileName = "GameEventInt",
    order = 1)]
public class GameEventInt : ScriptableObject//easy to administer event category
{
    private readonly List<GameEventIntListener> _listeners = new List<GameEventIntListener>();

    public void raise(int value)
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].onEventRaised(value);
        }
    }
    
    public void registerListener(GameEventIntListener listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void unRegisterListener(GameEventIntListener listener)
    {
        if (_listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }
    
}
