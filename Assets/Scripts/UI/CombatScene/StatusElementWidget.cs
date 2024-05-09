using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusElementWidget : MonoBehaviour
{
    [SerializeField] private Image icon;
    
    [SerializeField] private TextMeshProUGUI text;

    public string Type;// type of buff

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(StatusTemplate status, int value)
    {
        Type = status.Name;
        icon.sprite = status.Sprite;
        text.text = value.ToString();
    }

    public void Show()
    {
        _canvasGroup.DOFade(1.0f, 1.0f);
    }

    public void UpdateModifier(int value)
    {
        text.text = value.ToString();
    }

    public void FadeAndDestroy()
    {
        var seq = DOTween.Sequence();
        seq.Append(icon.DOFade(0, 0.3f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });

        text.DOFade(0, 0.3f);
    }
}
