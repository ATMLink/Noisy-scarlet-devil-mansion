using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class CardSelectionWithArrow : CardSelectionBase
{
    private Vector3 previousClickPosition;
    private Vector3 _originalCardPosition;
    private Quaternion _originalCardRotation;
    private int _originalCardSortingOrder;
    private const Ease _cardAnimationEase = Ease.OutBack;//dotween animation type

    private const float _cardDetectionOffset = 2.2f;//拖动卡牌以激活的阈值
    private const float _cardAnimationTime = 0.2f;
    private const float _cardCancelAnimationTime = 0.2f;

    private const float _selectedCardYOffset = -1.0f;
    private const float _attackCardInMiddlePositionX = 0;

    private AttackArrow _attackArrow;
    private bool isArrowCreated;

    protected override void Start()
    {
        base.Start();
        _attackArrow = FindFirstObjectByType<AttackArrow>();
    }

    private void Update()
    {
        if (cardDisplayManager.getIsCardMoving())
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))//卡牌是否被鼠标选中
        {
            detectCardSelection();
        }else if (Input.GetMouseButtonDown(1))
        {
            detectCardCancelSelection();
        }

        if (selectedCard != null)
        {
            updateCardAndTargetingArrow();
        }
    }
     
    /// <summary>
    /// 检测是否点击左键选中
    /// </summary>
    private void detectCardSelection()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, cardLayer);

        if (hitInfo.collider != null)
        {
            selectedCard = hitInfo.collider.gameObject;
            // _originalCardPosition = selectedCard.transform.position;
            // _originalCardRotation = selectedCard.transform.rotation;
            // _originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
            selectedCard.GetComponent<SortingGroup>().sortingOrder += 10;
            previousClickPosition = mousePosition;
        }
    }
    
    /// <summary>
    /// 取消选择
    /// </summary>
    private void detectCardCancelSelection()
    {
        if (selectedCard != null)
        {
            var card = selectedCard.GetComponent<CardObject>();
            selectedCard.transform.DOKill();
            
            card.reset(() =>
            {
                isArrowCreated = false;
                selectedCard = null;
            });
            
            _attackArrow.enableArrow(false);
        }
    }

    private void updateCardAndTargetingArrow()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var pullDistance = mousePosition.y - previousClickPosition.y;//拖动卡牌移动的距离

        if (!isArrowCreated && pullDistance > _cardDetectionOffset)//检查是否超过阈值
        {
            isArrowCreated = true;
            var position = selectedCard.transform.position;

            selectedCard.transform.DOKill();//重置位置

            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                selectedCard.transform.DOMove(new Vector3(_attackCardInMiddlePositionX, _selectedCardYOffset, position.z),
                    _cardAnimationTime);

                selectedCard.transform.DORotate(Vector3.zero, _cardAnimationTime);
            });

            sequence.AppendInterval(0.15f);
            sequence.OnComplete(() =>
            {
                _attackArrow.enableArrow(true);
            });
        }
    }
}
