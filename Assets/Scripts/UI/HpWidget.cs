using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpWidget : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpBarBackground;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI hpBorderText;

    private int maxValue;

    public void Initialize(IntVariable hp, int max)
    {
        maxValue = max;
        setHp(hp.value);
    }

    private void setHp(int value)
    {
        var newValue = value / (float)maxValue;

        hpBar.DOFillAmount(newValue, 0.2f).SetEase(Ease.InSine);

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(hpBarBackground.DOFillAmount(newValue, 0.2f));
        sequence.SetEase(Ease.InSine);

        hpText.text = $"{value.ToString()} / {maxValue.ToString()}";
        hpBorderText.text = $"{value.ToString()} / {maxValue.ToString()}";
    }

    public void onHpChanged(int value)
    {
        setHp(value);
        if (value <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
