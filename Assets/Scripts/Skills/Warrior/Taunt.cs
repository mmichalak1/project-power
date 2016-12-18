using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Taunt", menuName = "Game/Skills/Taunt")]
public class Taunt : Skill
{
    public TauntedBrain TauntedBrain;
    private TauntedBrain _myClone;
    public GameObject ParticleEffect;

    [Range(1, 5)]
    public int SkillDuration = 1;

    public override string Description()
    {
        return string.Format(_description, Power, TauntedBrain.Duration);
    }

    public override void Initialize(GameObject parent)
    {
        _myClone = Instantiate(TauntedBrain);
        _myClone.Target = parent;
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        _myClone = Instantiate(TauntedBrain);
        base.Initialize(data);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var attack = target.GetComponent<AttackController>();
        if (attack == null)
            return;
        attack.AddBrain(CreateBrainCopy(actor, target));
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        base.PerformAction(actor, target);
    }


    private AbstractBrain CreateBrainCopy(GameObject parent, GameObject target)
    {
        var _myCopy = Instantiate(TauntedBrain);
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        _myCopy.ParticleEffect = go;
        _myCopy.Target = parent;
        _myCopy.Duration = SkillDuration;
        _myCopy.Initialize(null);

        return _myCopy;
    }
}
