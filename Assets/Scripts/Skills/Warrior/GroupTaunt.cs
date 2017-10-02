using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Group Taunt", menuName = "Game/Skills/GroupTaunt")]
public class GroupTaunt : Skill
{

    public GameObject ParticleEffect;
    public TargetOffset tOffset;

    [Range(1, 5)]
    public int SkillDuration = 1;

    [Range(0, 100)]
    public int DamagePercentReduced = 20;

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
        var enemyGroup = target.transform.parent.GetComponent<EnemyGroup>();
        if (enemyGroup == null)
            return;

        var damageReductor = actor.AddComponent<DamageReductor>();
        damageReductor.Duration = SkillDuration;
        damageReductor.DamageReduced = DamagePercentReduced;

        foreach (var ene in enemyGroup.enemies)
        {

            var state = ene.GetComponent<EntityStatus>();
            if(state.Taunted)
            {
                var taunt = ene.GetComponent<TauntedEffect>();
                Destroy(taunt);
            }
            var newTaunt = ene.AddComponent<TauntedEffect>();
            newTaunt.Duration = SkillDuration;
            newTaunt.Target = actor;
            state.Taunted = true;
            Vector3 targetOffset = Vector3.zero;
            var targetingOffset = ene.GetComponent<TargetingOffset>();
            if (targetingOffset != null)
            {
                switch (tOffset)
                {
                    case TargetOffset.Belly: { targetOffset = targetingOffset.Belly; } break;
                    case TargetOffset.Head: { targetOffset = targetingOffset.Head; } break;
                }
            }
            GameObject go = Instantiate(ParticleEffect, ene.transform.position + targetOffset + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
            go.transform.parent = ene.transform;
            newTaunt.ParticleEffect = go;

        }

        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, SkillDuration, DamagePercentReduced);
    }

}
