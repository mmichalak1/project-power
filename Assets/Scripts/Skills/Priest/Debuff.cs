using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu (fileName = "Damage Debuff", menuName = "Game/Skills/Damage Debuff")]
public class Debuff : Skill {

    [Range(1,5)]
    public int Duration = 2;
    [Range(0, 70)]
    public int DebuffValue = 20;

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var debuff = target.AddComponent<DamageDebuff>();
        debuff.Duration = Duration;
        debuff.DebuffValue = DebuffValue;
        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, Duration, DebuffValue);
    }
}
