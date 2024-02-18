using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Sequence = Unity.VisualScripting.Sequence;

public class CardSelectionWithoutArrow : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Quaternion originalCardRotation;
    private int originalCardSortingOrder;

    private const float _cardCancelAnimationTime = 0.2f;//动画执行时间
    private const float _cardRecoverRotationTime = 0.3f;//动画执行时间
    private const Ease _cardAnimationEase = Ease.OutBack;//dotween animation type

    private const float _cardAboutToBePlayedOffsetY = 1.5f;
    private const float _CardAnimationTime = 0.4f;
    [SerializeField] private BoxCollider2D cardArea;

    private bool isCardAboutToBePlayed;
    
    private void Update()
    {
        if (cardDisplayManager.getIsCardMoving())//检测牌是否在执行移动的动画，若是则不能选择
            return;

        if (isCardAboutToBePlayed)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))//卡牌是否被鼠标选中
        {
            detectCardSelection();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            detectCardSelectionCancel();
        }
        
        
        if (selectedCard != null)//卡牌跟随鼠标移动
        {
            updateSelectedCard();
        }
    }

    /// <summary>
    /// 检测玩家是否点击左键选择卡牌
    /// </summary>
    private void detectCardSelection()
    {
        if (selectedCard != null)
            return;
        
        //屏幕左标转换为世界坐标
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition,Vector3.forward, Mathf.Infinity,
            cardLayer);

        if (hitInfo.collider != null)
        {
            var card = hitInfo.collider.GetComponent<CardObject>();
            var cardTemplate = card.template;

            if (!CardUtilities.cardHasTargetableEffect(cardTemplate))
            {
                selectedCard = hitInfo.collider.gameObject;
                originalCardPosition = selectedCard.transform.position;
                originalCardRotation = selectedCard.transform.rotation;
                originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
                selectedCard.transform.DORotateQuaternion(quaternion.Euler(Vector3.zero), _cardRecoverRotationTime);
            }
        }
        // else
        // {
        //     Debug.Log("no collider");
        // }
    }

    /// <summary>
    /// 检测玩家是否点击右键取消选择卡牌
    /// </summary>
    private void detectCardSelectionCancel()
    {
        if (selectedCard != null)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                selectedCard.transform.DOMove(originalCardPosition, _cardCancelAnimationTime).SetEase(_cardAnimationEase);
                selectedCard.transform.DORotate(originalCardRotation.eulerAngles, _cardCancelAnimationTime);
            });
            sequence.OnComplete(() =>
            {
                selectedCard.GetComponent<SortingGroup>().sortingOrder = originalCardSortingOrder;
                selectedCard = null;
            });
        }
    }

    /// <summary>
    /// 卡牌跟随鼠标移动
    /// </summary>
    private void updateSelectedCard()
    {
        //resolve the logic when mouse left key up
        if (Input.GetMouseButtonUp(0))
        {
            var card = selectedCard.GetComponent<CardObject>();
            
            //if selected card mouse left key is released and on ready to use state
            if (card.State == CardObject.CardState.AboutToBePlayed)
            {
                //turn off to pull card
                isCardAboutToBePlayed = true;
                
                //move card which is not attack card to the effect area
                var sequence = DOTween.Sequence();

                sequence.Append(selectedCard.transform.DOMove(cardArea.bounds.center, _CardAnimationTime))
                    .SetEase(_cardAnimationEase);
                sequence.AppendInterval(_CardAnimationTime + 0.1f);
                sequence.AppendCallback(() =>
                {
                    //play effect
                    playSelectedCard();
                    selectedCard = null;
                    isCardAboutToBePlayed = false;
                });
                //selectedCard.transform.DORotate(Vector3.zero, _cardAnimationTime);
                
            }

            // if when selected the card but regret, and card have not moved so far 
            // then reset card state put it to original position rotation
            else
            {
                card.setState(CardObject.CardState.InHand);
                selectedCard.GetComponent<CardObject>().reset(()=>selectedCard = null);
            }
        }
        
        if (selectedCard != null)
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedCard.transform.position = mousePosition;
            //Debug.Log("card position" + selectedCard.transform.position);

            var card = selectedCard.GetComponent<CardObject>();
            // detect non-attack card if moved far enough to change card state
            if (mousePosition.y > originalCardPosition.y + _cardAboutToBePlayedOffsetY)
            {
                card.setState(CardObject.CardState.AboutToBePlayed);
            }
            else
            {
                card.setState(CardObject.CardState.InHand);
            }
        }
    }
    
    // refactor base class method `playSelectedCard` to resolve non-attack card
    protected override void playSelectedCard()
    {
        base.playSelectedCard();

        var card = selectedCard.GetComponent<CardObject>().runtimeCard;
        effectResolutionManager.ResolveCardEffect(card);
        
    }

    public bool HasSelectedCard()
    {
        return selectedCard != null;
    }
}
 