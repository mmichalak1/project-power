using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AddResource", menuName = "Game/Skills/AddResource")]
public class AddResource : Skill
{
    [Range(1, 10)]
    public int ResourceAdded = 5;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void OnSkillPlanned(GameObject actor, GameObject target)
    {
        _cooldown = _baseCooldown;
        TurnManager.UpdateResource(0 - ResourceAdded);
        base.OnSkillPlanned(actor, target);
    }
}
