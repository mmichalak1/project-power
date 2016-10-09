using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Lighting Shield", menuName = "Game/Skills/Lighting Shield")]
public class LightingShield : Skill {

    [Range(1,5)]
    public int ShieldDuration = 2;
    [Range(20, 100)]
    public int DamagePercentReturned = 50;

    int baseDamage = 0; 

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var newTarget = target.AddComponent<GiveAwayDamage>();
        newTarget.Duration = ShieldDuration;
        newTarget.BaseDamage = Power;
        newTarget.DamagePercentReturned = DamagePercentReturned;
        base.PerformAction(actor, target);
    }
}
