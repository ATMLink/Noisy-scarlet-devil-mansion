using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/Actions/Shake Character Action",
    fileName = "ShakeCharacterAction",
    order = 10)]
public class ShakeCharacterAction : EffectAction
{
    public float duration;
    public Vector3 strenth;
    
    public override string getName()
    {
        return "Shake character";
    }

    public override void execute(GameObject gameObj)
    {
        gameObj.transform.DOShakePosition(duration, strenth);
    }
}
