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
        Debug.Log("start button pressed");
        LoadScene("CombatScene");
    }
    
    public void LoadScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
