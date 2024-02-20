using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventStatusListener : MonoBehaviour
{
    public GameEventStatus gameEventStatus;
    public UnityEvent<StatusTemplate, int> response;

    private void OnEnable()
    {
        gameEventStatus.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEventStatus.UnregisterListener(this);
    }

    public void OnEventRaised(StatusTemplate status, int value)
    {
        response.Invoke(status, value);
    }
}
