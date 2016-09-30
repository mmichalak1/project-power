using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Lighting Shield", menuName = "Game/Skills/Lighting Shield")]
public class LightingShield : Skill {

    [Range(1,5)]
    public int ShieldDuration = 2;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var newTarget = target.AddComponent<GiveAwayDamage>();
        newTarget.Duration = ShieldDuration;
        base.PerformAction(actor, target);
    }
}
