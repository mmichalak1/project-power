﻿using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "Group Taunt", menuName = "Game/Skills/GroupTaunt")]
public class GroupTaunt : Skill {

    public Assets.Scripts.ScriptableObjects.TauntedBrain TauntedBrain;
    private Assets.Scripts.ScriptableObjects.TauntedBrain _myCopy;
    public GameObject ParticleEffect;

    [Range(1, 5)]
    public int SkillDuration = 1;

    [Range(0, 100)]
    public int DamagePercentReduced = 20;

    public override void Initialize(GameObject parent)
    {
        _myCopy = Instantiate(TauntedBrain);
        _myCopy.Target = parent;
        _myCopy.Duration = SkillDuration;
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        _myCopy = Instantiate(TauntedBrain);
        _myCopy.Duration = SkillDuration;
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
            GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
            go.transform.parent = target.transform;
            trans.gameObject.GetComponent<AttackController>().AddBrain(_myCopy);
            _myCopy.ParticleEffect = go;
            
        }
       
        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, SkillDuration, DamagePercentReduced);
    }
}
