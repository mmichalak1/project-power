using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName ="Game/Skills/Basic Heal", fileName ="Basic Heal")]
public class Heal : Skill {

    public override void Initialize(GameObject parent)
    {

        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " heals " + target.name + " for " + _power + ".");
        target.GetComponent<Assets.Scripts.Interfaces.ICanBeHealed>().Heal(_power);
        base.PerformAction(actor, target);
    }

}
