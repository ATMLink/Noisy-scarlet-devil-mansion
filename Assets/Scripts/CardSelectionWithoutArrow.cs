using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CardSelectionWithoutArrow : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Quaternion originalCardRotation;
    private int originalCardSortingOrder;

    private void Update()
    {
        if (cardDisplayManager.getIsCardMoving())
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            detectCardSelection();
        }

        if (selectedCard != null)
        {
            updateSelectedCard();
        }
    }

    //检测玩家是否点击
    private void detectCardSelection()
    {
        if (selectedCard != null)
            return;

        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition,new Vector3(0,0,-1), Mathf.Infinity,
            cardLayer);

        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            Debug.Log("card is selected");
            originalCardPosition = selectedCard.transform.position;
            originalCardRotation = selectedCard.transform.rotation;
            originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
        }
    }

    private void updateSelectedCard()
    {
        if (selectedCard != null)
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedCard.transform.position = mousePosition;
            Debug.Log("card position" + selectedCard.transform.position);
        }
    }
}
 