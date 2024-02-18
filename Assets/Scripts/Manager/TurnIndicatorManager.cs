using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TurnIndicatorManager : MonoBehaviour
{
    [SerializeField] public GameObject turnIndicator;
    [SerializeField] public TextMeshProUGUI text;

    private const float InAnimDuration = 0.5f;
    private const float OutAnimDuration = 0.6f;
    private const float ShowcaseDuration = 2.0f;

    public void OnPlayerTurnBegan()
    {
        text.text = "Player Turn";
        var seq = DOTween.Sequence();
        seq.Append(turnIndicator.GetComponent<RectTransform>().DOScale(1f, InAnimDuration));
        seq.AppendInterval(ShowcaseDuration);
        seq.Append(turnIndicator.GetComponent<RectTransform>().DOScale(0.0f, OutAnimDuration));
    }
    
    public void OnEnemyTurnBegan()
    {
        text.text = "Enemy Turn";
        var seq = DOTween.Sequence();
        seq.Append(turnIndicator.GetComponent<RectTransform>().DOScale(1f, InAnimDuration));
        seq.AppendInterval(ShowcaseDuration);
        seq.Append(turnIndicator.GetComponent<RectTransform>().DOScale(0.0f, OutAnimDuration));
    }
}
