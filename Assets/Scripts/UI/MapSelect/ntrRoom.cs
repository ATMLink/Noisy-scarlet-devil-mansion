using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ntrRoom : MonoBehaviour
{
    public void OnButtonPressed()
    {
        Debug.Log("ntr room had pressed");
        SceneManager.LoadScene("CombatScene");
    }
}
