using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName ="Game/Skills/Basic Heal", fileName ="Basic Heal")]
public class Heal : Skill {
    public override string Description()
    {
        return string.Format(_description, Power);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " heals " + target.name + " for " + Power + ".");
        target.GetComponent<Assets.Scripts.Interfaces.ICanBeHealed>().Heal(Power);
        base.PerformAction(actor, target);
    }

}
