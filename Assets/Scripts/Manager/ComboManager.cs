using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private IntVariable combo;

    public void OnPlayerTurnBegan()
    {
        combo.setValue(0);
    }
}
