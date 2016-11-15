using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName ="Lighting Shield", menuName = "Game/Skills/Lighting Shield")]
public class LightingShield : Skill {

    [Range(1,5)]
    public int ShieldDuration = 2;
    [Range(20, 100)]
    public int DamagePercentReturned = 50;


    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var newTarget = target.AddComponent<GiveAwayDamage>();
        newTarget.Duration = ShieldDuration;
        newTarget.BaseDamage = Power;
        newTarget.DamagePercentReturned = DamagePercentReturned;
        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, ShieldDuration, Power, DamagePercentReturned);
    }
}
