using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textBorder;

    private int maxValue;

    public void Initialize(IntVariable sp)
    {
        maxValue = sp.Value;
        SetValue(sp.Value);
    }

    private void SetValue(int value)
    {
        text.text = $"{value.ToString()}/{maxValue.ToString()}";
        textBorder.text = text.text;
    }

    public void OnSpChanged(int value)
    {
        SetValue(value);
    }
}
