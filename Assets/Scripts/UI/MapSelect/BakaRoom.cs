using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BakaRoom : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("CombatSceneBaka");
    }
}
