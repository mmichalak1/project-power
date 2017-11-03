using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Frost", menuName = "Game/Skills/Frost")]
public class Frost : Skill
{

    public int StunDuration = 2;
    public GameObject ParticleEffect;
    public TargetOffset tOffset;

    public override string Description()
    {
        return string.Format(_description, StunDuration);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " stuns " + target.name + " for " + (StunDuration) + " for turns.");
        var state = target.GetComponent<EntityStatus>();
        StunnedEffect newStun = null;
        //check if stun is already on target if so add stun duration to it
        if (state.Stunned)
        {
            newStun = target.GetComponent<StunnedEffect>();
            newStun.Duration += StunDuration;
        }
        else
        {
            newStun = target.AddComponent<StunnedEffect>();
            newStun.Duration = StunDuration;
            state.Stunned = true;
            Vector3 targetOffset = Vector3.zero;
            var targetingOffset = target.GetComponent<TargetingOffset>();
            if (targetingOffset != null)
            {
                switch (tOffset)
                {
                    case TargetOffset.Belly: { targetOffset = targetingOffset.Belly; } break;
                    case TargetOffset.Head: { targetOffset = targetingOffset.Head; } break;
                }
            }
            GameObject go = Instantiate(ParticleEffect, target.transform.position + targetOffset + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
            go.transform.parent = target.transform;
            newStun.ParticleEffect = go;
        }
        base.PerformAction(actor, target);
    }
}
