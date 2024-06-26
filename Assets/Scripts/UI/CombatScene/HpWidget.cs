using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpWidget : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image maxHPBar;
    [SerializeField] private Image hpBarBackground;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI hpBorderText;

    [SerializeField] private GameObject shieldGroup;
    [SerializeField] private TextMeshProUGUI shieldTetxBorder;
    [SerializeField] private TextMeshProUGUI shieldText;

    private IntVariable maxValue;
    private IntVariable currentValue;
    private int absoluteMaxValue;
    
    public void Initialize(IntVariable hp, IntVariable max, IntVariable shield, int absoluteMax)
    {
        maxValue = max;
        currentValue = hp;
        absoluteMaxValue = absoluteMax;
        setHp(hp.Value);
        setMaxHP(max.Value);
        setShield(shield.Value);
    }

    private void setHp(int value)
    {
        var newValue = value / (float)absoluteMaxValue;

        hpBar.DOFillAmount(newValue, 0.2f).SetEase(Ease.InSine);

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(hpBarBackground.DOFillAmount(newValue, 0.2f));
        sequence.SetEase(Ease.InSine);

        hpText.text = $"{value.ToString()} / {maxValue.Value.ToString()}";
        hpBorderText.text = $"{value.ToString()} / {maxValue.Value.ToString()}";
    }

    private void setMaxHP(int value)
    {
        var newValue = value / (float)absoluteMaxValue;

        maxHPBar.DOFillAmount(newValue, 0.2f).SetEase(Ease.InSine);

        hpText.text = $"{currentValue.Value.ToString()} / {value.ToString()}";
        hpBorderText.text = $"{currentValue.Value.ToString()} / {value.ToString()}";
    }
    
    private void setShield(int value)
    {
        shieldText.text = $"{value.ToString()}";
        shieldTetxBorder.text = $"{value.ToString()}";
        setShieldActive(value>0);
    }
    
    private void setShieldActive(bool shieldActive)
    {
        shieldGroup.SetActive(shieldActive);
    }
    
    public void onHpChanged(int value)
    {
        setHp(value);
        // if (value <= 0)
        // {
        //     gameObject.SetActive(false);
        // }
    }

    public void OnMaxHPChanged(int value)
    {
        setMaxHP(value);
    }

    public void OnHPChangedToDisableWidget(int value)
    {
        if(value<=0)
            gameObject.SetActive(false);
    }
    public void onShieldChanged(int value)
    {
        setShield(value);
    }
}
