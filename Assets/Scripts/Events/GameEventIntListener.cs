using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameEventIntListener : MonoBehaviour
{
    //listening event
    public GameEventInt eventInt;
    
    //react when event raised
    public UnityEvent<int> response;

    private void OnEnable()
    {
        eventInt.registerListener(this);
    }

    private void OnDisable()
    {
        eventInt.unRegisterListener(this);
    }

    public void onEventRaised(int value)
    {
        response.Invoke(value);
    }
}
