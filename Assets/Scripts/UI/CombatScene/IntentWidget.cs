using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntentWidget : MonoBehaviour
{
    [SerializeField] private Image intentImage;
    [SerializeField] private TextMeshProUGUI amountText;

    private const float InitialDelay = 1.25f;
    private const float FadeInDuration = 0.8f;
    private const float FadeOutDuration = 0.5f;

    public void OnEnemyTurnBegan()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(InitialDelay);
        // because enemy has already moved so that there is no need to show intent
        sequence.Append(intentImage.DOFade(0.0f, FadeOutDuration));// fade out image

        sequence = DOTween.Sequence();
        sequence.AppendInterval(InitialDelay);
        sequence.Append(amountText.DOFade(0.0f, FadeOutDuration));// fade out text
    }

    public void OnIntentChange(Sprite sprite, int value)
    {
        intentImage.sprite = sprite;
        intentImage.SetNativeSize();
        amountText.text = value.ToString();

        intentImage.DOFade(1.0f, FadeInDuration);
        amountText.DOFade(1.0f, FadeInDuration);
    }

    public void OnHpChanged(int hp)
    {
        if (hp <= 0)
        {
            intentImage.DOFade(0.0f, FadeOutDuration);
            amountText.DOFade(0.0f, FadeOutDuration);
        }
    }
}
