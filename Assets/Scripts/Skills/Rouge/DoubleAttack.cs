using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Double Attack", menuName ="Game/Skills/Double Attack")]
public class DoubleAttack : Skill {

    public int AttackCount = 2;
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " attacks " + target.name + " " + AttackCount + " times.");
        for (int i = 0; i < AttackCount; i++)
        {
            target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power);
        }
        base.PerformAction(actor, target);
    }
}
