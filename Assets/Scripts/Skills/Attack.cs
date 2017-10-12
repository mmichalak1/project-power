using UnityEngine;
using Assets.Scripts.Interfaces;

[CreateAssetMenu(menuName = "Game/Skills/BasicAttack")]
public class Attack : Skill {
    public override string Description()
    {
        return string.Format(_description, Power);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        int damage = actor.GetComponent<IProvideStatistics>().GetDamage();

        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(damage, actor);
        base.PerformAction(actor, target);
    }
}
