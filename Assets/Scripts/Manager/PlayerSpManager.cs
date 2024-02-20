using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpManager : MonoBehaviour
{
    public IntVariable playerSpVariable;

    private int defaultSp = 3;
    private int maxSp = 3;

    public int GetMaxSp()
    {
        return maxSp;
    }

    public void SetMaxSp(int maxSp)
    {
        this.maxSp = maxSp;
    }
    public void SetDefaultSp(int value)
    {
        defaultSp = value;
    }

    public void OnPlayerTurnBegan()
    {
        playerSpVariable.setValue(defaultSp);
    }
}
