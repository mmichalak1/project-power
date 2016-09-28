using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AddResource", menuName = "Game/Skills/AddResource")]
public class AddResource : Skill
{

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void OnSkillPlanned()
    {
        _cooldown = _baseCooldown;
        TurnManager.UpdateResource(0 - _power);
        base.OnSkillPlanned();
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {

    }
}
