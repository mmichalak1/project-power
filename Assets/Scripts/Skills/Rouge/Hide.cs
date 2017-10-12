using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Hide", menuName = "Game/Skills/Hide")]
public class Hide : Skill {

    public int Duration = 2;

    public override string Description()
    {
        return string.Format(_description, Duration);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var status = actor.GetComponent<EntityStatus>();
        if(!status.Targetable)
        {
            Debug.Log(actor.name + " already untargetable.");
            base.PerformAction(actor, target);
            return;
        }
        var comp = actor.AddComponent<Untargetable>();
        comp.Duration = Duration;

        status.Targetable = false;
        base.PerformAction(actor, target);
    }
}
