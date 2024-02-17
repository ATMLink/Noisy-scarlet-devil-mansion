using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        gameEvent.registerListener(this);
    }

    private void OnDisable()
    {
        gameEvent.unRegisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
