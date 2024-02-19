using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntentChangeEventListener : MonoBehaviour
{
    public IntentChangeEvent intentChangeEvent;
    public UnityEvent<Sprite, int> response;

    private void OnEnable()
    {
        intentChangeEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        intentChangeEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Sprite sprite, int value)
    {
        response.Invoke(sprite, value);
    }
}
