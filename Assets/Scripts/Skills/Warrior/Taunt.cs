using UnityEngine;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(fileName = "Taunt", menuName = "Game/Skills/Taunt")]
public class Taunt : Skill
{
    public TauntedBrain TauntedBrain;
    private TauntedBrain _myClone;

    public override void Initialize(GameObject parent)
    {
        _myClone = Instantiate(TauntedBrain);
        _myClone.Target = parent;
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var attack = target.GetComponent<AttackController>();
        if (attack == null)
            return;
        attack.AddBrain(_myClone);
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        base.PerformAction(actor, target);
    }
}
