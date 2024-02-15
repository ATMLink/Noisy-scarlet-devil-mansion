using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Actions/Play Particles Action",
    fileName = "PlayParticlesAction",
    order = 9)]
public class PlayParticlesAction : EffectAction
{
    public ParticleSystem particles;
    public Vector3 offset;
    
    public override string getName()
    {
        return "Play Particles";
    }

    public override void execute(GameObject gameObj)
    {
        var position = gameObj.transform.position;
        var particles = Instantiate(this.particles);
        particles.transform.position = position + offset;
        particles.Play();

        var autoDestroy = particles.gameObject.AddComponent<AutoDestroy>();
        autoDestroy.duration = 2.0f;
    }
}
