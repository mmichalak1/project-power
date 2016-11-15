using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName ="Smash", menuName = "Game/Skills/SMASH")]
public class Smash : Skill {

    [Range(100, 300)]
    public int PowerPercentage = 120;

    public override string Description()
    {
        return string.Format(_description, (Power * PowerPercentage) / 100);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " SMASHES " + target.name + " for " + (Power * PowerPercentage) / 100 + " damage.");
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((Power * PowerPercentage) / 100, actor);
        base.PerformAction(actor, target);
    }
}
