using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Frost", menuName = "Game/Skills/Frost")]
public class Frost : Skill
{

    public int StunDuration = 2;
    public StunnedBrain StunnedBrain;
    public GameObject ParticleEffect;

    public override string Description()
    {
        return string.Format(_description, StunDuration);
    }

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
       
        var attack = target.GetComponent<AttackController>();
        if (attack == null)
            return;
        Debug.Log(actor.name + " stuns " + target.name + " for " + (StunDuration) + " for turns.");
        attack.AddBrain(CreateStunnedBrain(actor, target));
        base.PerformAction(actor, target);
    }

    private AbstractBrain CreateStunnedBrain(GameObject actor, GameObject target)
    {
        var copy = Instantiate(StunnedBrain);
        copy.SetDuration(StunDuration);
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        copy.ParticleEffect = go;

        return copy;
    }

}
