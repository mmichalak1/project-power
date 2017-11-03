using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "AddResource", menuName = "Game/Skills/AddResource")]
public class AddResource : Skill, InstantSkill, NotTargetableSkill
{
    [Range(1, 10)]
    public int ResourceAdded = 5;

    public override string Description()
    {
        return string.Format(_description, ResourceAdded);
    }

    public GameObject GetTarget(SheepGroupManager sheep, EnemyGroup enemies)
    {
        return sheep.Sheep[0];
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        SystemAccessor.GetSystem<BattleResourcesController>().MoveFromTakenToAvailable(ResourceAdded);
        base.PerformAction(actor, target);
    }
}
