using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName ="Double Attack", menuName ="Game/Skills/Double Attack")]
public class DoubleAttack : Skill {

    public int AttackCount = 2;

    public override string Description()
    {
        return string.Format(_description, AttackCount, Power);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " attacks " + target.name + " " + AttackCount + " times.");
        for (int i = 0; i < AttackCount; i++)
        {
            target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        }
        base.PerformAction(actor, target);
    }
}
