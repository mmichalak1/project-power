﻿using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Banana Throw", menuName = "Game/Skills/Banana Throw")]
public class BananaThrow : Skill {

    public int StunDuration = 1;
    public StunnedBrain StunnedBrain;
    public GameObject ParticleEffect;
    public TargetOffset tOffset;

    public override string Description()
    {
        return string.Format(_description, StunDuration);
    }

    // Use this for initialization
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        base.Initialize(data);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " stuns " + target.name + "for " + StunDuration + " turns.");
        target.GetComponent<AttackController>().AddBrain(CreateStunnedBrain(actor, target));
        base.PerformAction(actor, target);
    }


    private AbstractBrain CreateStunnedBrain(GameObject actor, GameObject target)
    {
        var copy = Instantiate(StunnedBrain);
        copy.SetDuration(StunDuration);
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
        copy.ParticleEffect = go;

        return copy;
    }



}
