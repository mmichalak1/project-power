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
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        base.PerformAction(actor, target);
    }
}
