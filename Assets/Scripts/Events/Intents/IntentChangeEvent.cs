using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Events/Game Event (Intent change)",
    fileName = "GameEventIntentChange",
    order = 3)]
public class IntentChangeEvent : ScriptableObject
{
    private readonly List<IntentChangeEventListener> _listeners = new List<IntentChangeEventListener>();

    public void Raise(Sprite sprite, int value)
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(sprite, value);
    }

    public void RegisterListener(IntentChangeEventListener listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void UnregisterListener(IntentChangeEventListener listener)
    {
        if (_listeners.Contains(listener))
        {
            _listeners.Remove(listener);
        }
    }
}
