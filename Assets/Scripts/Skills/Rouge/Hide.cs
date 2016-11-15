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
        var comp = actor.AddComponent<Untargetable>();
        comp.Duration = Duration;
        base.PerformAction(actor, target);
    }
}
