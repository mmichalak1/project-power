using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Game/Skills/BasicAttack")]
public class Attack : Skill {
    public override string Description()
    {
        return string.Format(_description, Power);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        //Debug.Log(actor.name + " attacked " + target.name + " for " + _power + " damage.");
        float actualPower = Power;
        var debuffs = actor.GetComponents<DamageDebuff>();
        foreach (var item in debuffs)
            actualPower *= item.DebuffValue / 100.0f;
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((int)actualPower, actor);
        base.PerformAction(actor, target);
    }
}
