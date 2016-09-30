using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Game/Skills/BasicAttack")]
public class Attack : Skill {

	// Use this for initialization
	public override void Initialize (GameObject parent) {
        base.Initialize(parent);
	}

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " attacked " + target.name + " for " + _power + " damage.");
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(_power, actor);
        base.PerformAction(actor, target);
    }
}
