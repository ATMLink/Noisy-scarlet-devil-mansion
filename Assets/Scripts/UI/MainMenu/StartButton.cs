using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("MapSelectTemporary");
    }
    
}
