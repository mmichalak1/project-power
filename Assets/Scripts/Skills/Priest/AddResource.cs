﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AddResource", menuName = "Game/Skills/AddResource")]
public class AddResource : Skill
{

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void OnSkillPlanned(GameObject actor, GameObject target)
    {
        _cooldown = _baseCooldown;
        TurnManager.UpdateResource(0 - _power);
        base.OnSkillPlanned(actor, target);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {

    }
}