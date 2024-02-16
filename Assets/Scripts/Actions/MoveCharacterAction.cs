using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Actions/Move Character Action",
    fileName = "MoveCharacterAction",
    order = 7)]
public class MoveCharacterAction : EffectAction
{
    public float duration;
    public Vector3 offset;
    
    public override string getName()
    {
        return "Move Character";
    }

    public override void execute(GameObject gameObj)
    {
        var originalPosition = gameObj.transform.position;
        var sequence = DOTween.Sequence();
        sequence.Append(gameObj.transform.DOMove(originalPosition + offset, duration));
        sequence.Append(gameObj.transform.DOMove(originalPosition, duration * 2));//when back to original position, easing
    }
}
