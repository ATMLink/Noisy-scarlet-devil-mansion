using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    private const int positionNumber = 20;
    private const int rotationNumber = 20;
    private const int sortingOrdersNumber = 20;

    private CardsManager _cardsManager;
    private List<Vector3> _position;
    private List<Quaternion> _rotation;
    private List<int> _sortingOrder;

    private readonly List<GameObject> _handCards = new List<GameObject>(positionNumber);
    private const float radius = 16.0f;
    private readonly Vector3 _center = new(-15.0f, 18.5f, 0.0f);
    private readonly Vector3 _originalCardScale = new(-0.5f, 0.5f, 1.0f);

    private bool _isMoving;

    public void initialize(CardsManager cardsManager)
    {
        _cardsManager = cardsManager;
    }

    private void Start()
    {
        _position = new(positionNumber);
        _rotation = new(rotationNumber);
        _sortingOrder = new(sortingOrdersNumber);
    }

    public void createHandCards(List<RuntimeCard> cardsInHand)
    {
        var drawnCards = new List<GameObject>(cardsInHand.Count);

        foreach (var card in cardsInHand)
        {
            var cardGameObject = createCardGameObject(card);
            _handCards.Add(cardGameObject);
            drawnCards.Add(cardGameObject);
        }
    }

    private GameObject createCardGameObject(RuntimeCard card)
    {
        var gameObj = _cardsManager.getObject();
        var cardObject = gameObj.GetComponent<CardObject>();
        cardObject.SetInfo(card);

        gameObj.transform.position = Vector3.zero;
        gameObj.transform.localScale = Vector3.zero;
        
        return gameObj;
    }
}
