using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CardDisplayManager : MonoBehaviour
{
    private const int positionNumber = 20;
    private const int rotationNumber = 20;
    private const int sortingOrdersNumber = 20;

    private CardsManager _cardsManager;
    
    // private List<Vector3> _positions=new(positionNumber);
    // private List<Quaternion> _rotations= new(rotationNumber);
    // private List<int> _sortingOrders=new(sortingOrdersNumber);

    private List<Vector3> _positions;
    private List<Quaternion> _rotations;
    private List<int> _sortingOrders;
    
    private readonly List<GameObject> _handCards = new List<GameObject>(positionNumber);
    
    private const float radius = 18.0f;
    private readonly Vector3 _center = new(0.0f, -22.3f, 0.0f);
    private readonly Vector3 _originalCardScale = new(0.6f, 0.6f, 1.0f);

    private bool _isCardMoving;

    public static float cardToDiscardPileAnimationTime = 0.3f;

    public void initialize(CardsManager cardsManager)
    {
        _cardsManager = cardsManager;
    }

    private void Awake()
    {
        _positions = new(positionNumber);
        _rotations = new(rotationNumber);
        _sortingOrders = new(sortingOrdersNumber);
    }

    /// <summary>
    /// 生成手牌
    /// </summary>
    /// <param name="cardsInHand"></param>
    public void createHandCards(List<RuntimeCard> cardsInHand)
    {
        var drawnCards = new List<GameObject>(cardsInHand.Count);

        foreach (var card in cardsInHand)
        {
            var cardGameObject = createCardGameObject(card);
            cardGameObject.transform.position = new Vector3(8.5f, 0.0f, 0.0f);
            _handCards.Add(cardGameObject);
            drawnCards.Add(cardGameObject);
        }
        
        putDeckCardsToHand(drawnCards);//执行卡牌动画
    }

    /// <summary>
    /// 克隆game object出来
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    private GameObject createCardGameObject(RuntimeCard card)
    {
        var gameObj = _cardsManager.getObject();
        var cardObject = gameObj.GetComponent<CardObject>();
        cardObject.SetInfo(card);

        gameObj.transform.position = Vector3.zero;
        gameObj.transform.localScale = Vector3.zero;
        
        return gameObj;
    }

    /// <summary>
    /// dynamically put deck cards to hand by DoTween
    /// </summary>
    /// <param name="drawnCards"></param>
    private void putDeckCardsToHand(List<GameObject> drawnCards)
    {
        _isCardMoving = true;
        
        organizeHandCards();

        var interval = 0.0f;//the time span between each card moves幕间

        for (var i = 0; i < _handCards.Count; i++)
        {
            var j = i;

            const float time = 0.5f;//动画完成时间
            var card = _handCards[i];

            if (drawnCards.Contains(card))//check if the card was in hand already
            {
                var cardObject = card.GetComponent<CardObject>();

                var seq = DOTween.Sequence();//序列播放
                seq.AppendInterval(interval);
                seq.AppendCallback(() =>
                {
                    var move = card.transform.DOMove(_positions[j], time).OnComplete(() =>
                    {
                        cardObject.saveTransform(_positions[j], _rotations[j]);
                    });//move
                    
                    cardObject.transform.DORotateQuaternion(_rotations[j], time);
                    cardObject.transform.DOScale(_originalCardScale, time);

                    if (j == _handCards.Count - 1)
                        move.OnComplete(() =>
                        {
                            _isCardMoving = false;
                            cardObject.saveTransform(_positions[j], _rotations[j]);
                        });//标志动画结束
                });
            }

            card.GetComponent<SortingGroup>().sortingOrder = _sortingOrders[i];//保证先进的在最底下

            interval += 0.2f;//每张牌动画间隔
        }
    }

    /// <summary>
    /// assign each card for display animation parameter
    /// </summary>
    private void organizeHandCards()
    {
        _positions.Clear();
        _rotations.Clear();
        _sortingOrders.Clear();

        const float angle = 5.0f;
        var cardAngle = (_handCards.Count - 1) * angle / 2;// total angle
        var z = 0.0f;

        for (var i = 0; i < _handCards.Count; ++i)
        {
            //rotate
            var rotation = Quaternion.Euler(0, 0, cardAngle - i * angle);//calculate the euler angle
            _rotations.Add(rotation);
            
            //Move
            z -= 0.1f;
            var position = calculateCardPosition(cardAngle-i*angle);
            position.z = z;
            _positions.Add(position);
            
            //sorting layer setting
            _sortingOrders.Add(i);//设定sorting layer 加入越早放越低（被盖住）
        }
    }

    /// <summary>
    /// reorganize hand cards when player used a card
    /// </summary>
    /// <param name="selectedCard"></param>
    public void reorganizeHandCards(GameObject selectedCard)
    {
        _handCards.Remove(selectedCard);
        
        //reorganize cards position
        organizeHandCards();
        
        //dynamically reorganize cards graphical position by DoTween
        for (var i = 0; i < _handCards.Count; i++)
        {
            var card = _handCards[i];
            const float time = 0.3f;
            card.transform.DOMove(_positions[i], time);
            card.transform.DORotateQuaternion(_rotations[i], time);
            card.GetComponent<SortingGroup>().sortingOrder = _sortingOrders[i];
            card.GetComponent<CardObject>().saveTransform(_positions[i], _rotations[i]);
        }
    }

    /// <summary>
    /// 根据角度计算在这个弧形中每张卡应该在的位置
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private Vector3 calculateCardPosition(float angle)
    {
        return new Vector3(_center.x - radius * Mathf.Sin(Mathf.Deg2Rad * angle),
            _center.y + radius*Mathf.Cos(Mathf.Deg2Rad*angle),
            0.0f
            );//z 单独设定
    }
    
    public bool getIsCardMoving()
    {
        return _isCardMoving;
    }

    public void moveCardToDiscardPile(GameObject gameObj)
    {
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            gameObj.transform.DOScale(Vector3.zero, cardToDiscardPileAnimationTime).OnComplete(() =>
            {
                gameObj.GetComponent<CardsManager.ManagedPoolObject>().cardsManager.returnObject(gameObj);
            });
        });
        sequence.AppendCallback(() =>
        {
            _handCards.Remove(gameObj);
        });
    }
}
