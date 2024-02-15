using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Actions/Play Animation Action",
    fileName = "PlayAnimationAction",
    order = 8)]
public class PlayAnimationAction : EffectAction
{
    public Animator animator;
    public Vector3 offset;
    
    public override string getName()
    {
        return "Play animation";
    }

    public override void execute(GameObject gameObj)
    {
        var position = gameObj.transform.position;
        var animator = Instantiate(this.animator);
        animator.transform.position = position + offset;

        var autoDestroy = animator.gameObject.AddComponent<AutoDestroy>();
        autoDestroy.duration = 2.0f;
    }
}
