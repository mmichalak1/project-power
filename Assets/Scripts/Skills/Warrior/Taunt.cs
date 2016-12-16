using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Taunt", menuName = "Game/Skills/Taunt")]
public class Taunt : Skill
{
    public TauntedBrain TauntedBrain;
    private TauntedBrain _myClone;
    public GameObject ParticleEffect;

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
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        _myClone.ParticleEffect = go;
        var attack = target.GetComponent<AttackController>();
        if (attack == null)
            return;
        attack.AddBrain(_myClone);
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        base.PerformAction(actor, target);
    }
}
