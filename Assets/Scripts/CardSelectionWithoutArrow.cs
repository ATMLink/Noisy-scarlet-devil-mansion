using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSelectionWithoutArrow : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Vector3 originalCardRotation;
    private int originalCardSortingOrder;

    private void Update()
    {
        if (cardDisplayManager._isCardMoving)
        {
            
        }
    }
}
