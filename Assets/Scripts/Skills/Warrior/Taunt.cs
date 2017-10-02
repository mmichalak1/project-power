using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Taunt", menuName = "Game/Skills/Taunt")]
public class Taunt : Skill
{
    public GameObject ParticleEffect;
    public TargetOffset tOffset;
    public int TauntDuration;

    [Range(1, 5)]
    public int SkillDuration = 1;

    public override string Description()
    {
        return string.Format(_description, Power, TauntDuration);
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
        var targetState = target.GetComponent<EntityStatus>();
        //remove exisitng taunt
        if(targetState.Taunted)
        {
            var taunt = target.GetComponent<TauntedEffect>();
            Destroy(taunt);
        }
        //add new taunt
        targetState.Taunted = true;
        var newTaunt = target.AddComponent<TauntedEffect>();
        newTaunt.Duration = SkillDuration;
        newTaunt.Target = actor;
        //create effect
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
        //add reference to newTaunt effect
        newTaunt.ParticleEffect = go;
        //deal damage
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        base.PerformAction(actor, target);
    }
}
