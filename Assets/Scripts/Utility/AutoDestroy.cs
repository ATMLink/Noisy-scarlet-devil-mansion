using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class AutoDestroy : MonoBehaviour
{
    public float duration;
    
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
