using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using UnityEngine.SceneManagement;

public class EndBattlePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button endOfGameButton;
    private CanvasGroup _canvasGroup;

    private const string VictoryText = "Victory";
    private const string DefeatText = "Defeat";

    private const float FadeInTime = 0.4f;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1.0f, FadeInTime);
    }

    public void SetVictoryText()
    {
        titleText.text = VictoryText;
        descriptionText.text = string.Empty;
    }
    
    public void SetDefeatText()
    {
        titleText.text = DefeatText;
        descriptionText.text = string.Empty;
    }

    public void OnEndOfGameBattlePressed()
    {
        // EditorApplication.isPlaying = false;
        SceneManager.LoadScene("MainMenu");
    }
}
