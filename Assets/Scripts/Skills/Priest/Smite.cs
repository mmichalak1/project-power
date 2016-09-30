using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Smite", menuName ="Game/Skills/Smite")]
public class Smite : Skill {

    [Range(0,100)]
    public int HealPercentage = 10;
    [Range(0, 100)]
    public int DamagePercentage = 80;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " smites " + target.name + " for " + ((Power * DamagePercentage) / 100) + " damage and heals himself for " + ((Power * HealPercentage) / 100));
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((Power * DamagePercentage) / 100, actor);
        actor.GetComponent<Assets.Scripts.Interfaces.ICanBeHealed>().Heal((Power * HealPercentage) / 100);
        base.PerformAction(actor, target);
    }
}
