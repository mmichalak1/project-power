using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Skills/BasicAttack")]
public class Attack : Skill {

	// Use this for initialization
	public override void Initialize (GameObject parent) {
        _action = PerformAttack;
        base.Initialize(parent);
	}
	

    private void PerformAttack(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " attacked " + target.name + " for " + _power + " damage.");
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(_power);
    }
}
