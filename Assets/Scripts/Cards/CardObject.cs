using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class CardObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro typeText;
    [SerializeField] private TextMeshPro descriptionText;

    [SerializeField] private SpriteRenderer illustration;

    public CardTemplate template;
    public RuntimeCard runtimeCard;

    private Vector3 _savedPosition;
    private Quaternion _savedRoation;
    private int _savedSortingOrder;

    private float _animationTime = 0.2f;

    private SortingGroup _sortingGroup;

    public enum CardState
    {
        InHand,// card in hand state
        AboutToBePlayed// card ready to use
    }

    private CardState _currentState;
    public CardState State => _currentState;

    private void OnEnable()
    {
        setState(CardState.InHand);
    }

    public void setState(CardState state)
    {
        _currentState = state;
    }

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
    }

    private void Start()
    {
        var testCard = new RuntimeCard
        {
            Template = template
        };
        SetInfo(testCard);
    }
    public void SetInfo(RuntimeCard card)
    {
        runtimeCard = card;
        template = card.Template;
        costText.text = template.cost.ToString();
        nameText.text = template.name;
        typeText.text = template.type.typeName;
        var builder = new StringBuilder();
        descriptionText.text = builder.ToString();
        illustration.sprite = template.illustration;
    }

    public void saveTransform(Vector3 position, Quaternion rotation)
    {
        _savedPosition = position;
        _savedRoation = rotation;
        _savedSortingOrder = _sortingGroup.sortingOrder;
    }
    public void reset(Action OnComplete) 
    {
        transform.DOMove(_savedPosition, _animationTime).SetEase(Ease.OutBack);
        transform.DORotateQuaternion(_savedRoation, _animationTime);
        _sortingGroup.sortingOrder = _savedSortingOrder;
        OnComplete();
    }
}
