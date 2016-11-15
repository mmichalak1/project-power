using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "AddResource", menuName = "Game/Skills/AddResource")]
public class AddResource : Skill
{
    [Range(1, 10)]
    public int ResourceAdded = 5;

    public override string Description()
    {
        return string.Format(_description, ResourceAdded);
    }

    public override void OnSkillPlanned(GameObject actor, GameObject target)
    {
        _cooldown = CooldownBase;
        TurnManager.UpdateResource(0 - ResourceAdded);
        base.OnSkillPlanned(actor, target);
    }
}
