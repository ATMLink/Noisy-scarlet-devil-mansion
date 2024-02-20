using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpManager : MonoBehaviour
{
    public IntVariable playerSpVariable;

    private int defaultSp = 3;

    public void SetDefaultSp(int value)
    {
        defaultSp = value;
    }

    public void OnPlayerTurnBegan()
    {
        playerSpVariable.setValue(defaultSp);
    }
}
