using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEvent<T> eventSO;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised -= OnEventRaised;
    }

    private void OnEventRaised(T value)
    {
        response.Invoke(value);
    }
}
