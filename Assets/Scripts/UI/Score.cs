using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private IntVariable overDamage;
    [SerializeField] private IntVariable recover;
        
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI overDamageText;
    [SerializeField] private TextMeshProUGUI recoverText;

    private void Start()
    {
        init();
    }

    private void init()
    {
        overDamage.setValue(0);
        recover.setValue(0);
    }
    private void SetOverDamageValue(int value)
    {
        overDamageText.text = $"over damage: {overDamage.Value.ToString()}";
    }

    private void SetRecoverValue(int value)
    {
        recoverText.text = $"recovery amount: {recover.Value.ToString()}";
    }

    private void SetScoreValue(int overDamage, int recover)
    {
        scoreText.text = $"Score: {(overDamage * 4 - recover)}";
    }

    public void OnOverDamageChanged(int value)
    {
        SetOverDamageValue(value);
        SetScoreValue(overDamage.Value, recover.Value);
    }

    public void OnRecoverChanged(int value)
    {
        SetRecoverValue(value);
        SetScoreValue(overDamage.Value, recover.Value);
    }
}
