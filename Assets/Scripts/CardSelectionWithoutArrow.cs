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
        if (cardDisplayManager.getIsCardMoving())//检测牌是否在执行移动的动画，若是则不能选择
            return;
        
        if (Input.GetMouseButtonDown(0))//卡牌是否被鼠标选中
        {
            detectCardSelection();
        }

        if (selectedCard != null)//卡牌跟随鼠标移动
        {
            updateSelectedCard();
        }
    }

    //检测玩家是否点击
    private void detectCardSelection()
    {
        if (selectedCard != null)
            return;
        
        //屏幕左标转换为世界坐标
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // 在控制台中打印世界坐标
        Debug.Log("Mouse world position: " + mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition,Vector3.forward, Mathf.Infinity,
            cardLayer);

        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            Debug.Log("card is selected");
            originalCardPosition = selectedCard.transform.position;
            originalCardRotation = selectedCard.transform.rotation;
            originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
        }
        else
        {
            Debug.Log("no collider");
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
 