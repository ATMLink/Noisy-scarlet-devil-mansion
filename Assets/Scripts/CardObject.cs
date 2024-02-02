using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class CardObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro typeText;
    [SerializeField] private TextMeshPro descriptionText;

    [SerializeField] private SpriteRenderer illustration;

    public CardTemplate template;
    public RuntimeCard runtimeCard;

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
}
