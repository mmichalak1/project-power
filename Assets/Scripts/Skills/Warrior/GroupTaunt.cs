using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Group Taunt", menuName = "Game/Skills/GroupTaunt")]
public class GroupTaunt : Skill
{

    public TauntedBrain TauntedBrain;
    public GameObject ParticleEffect;

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
        var enemyGroup = target.transform.parent.GetComponent<WolfGroupManager>();
        if (enemyGroup == null)
            return;

        var damageReductor = actor.AddComponent<DamageReductor>();
        damageReductor.Duration = SkillDuration;
        damageReductor.DamageReduced = DamagePercentReduced;

        foreach (var trans in enemyGroup.enemies)
        {

            trans.gameObject.GetComponent<AttackController>().AddBrain(CreateBrainCopy(_parent, trans));
        }

        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, SkillDuration, DamagePercentReduced);
    }

    private AbstractBrain CreateBrainCopy(GameObject parent, GameObject target)
    {
        var _myCopy = Instantiate(TauntedBrain);
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        _myCopy.ParticleEffect = go;
        _myCopy.Target = parent;
        _myCopy.Duration = SkillDuration;
        _myCopy.Initialize(null);

        return _myCopy;
    }

}
