using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YasakaRoom : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("CombatScenYakami");
    }
}
