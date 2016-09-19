using UnityEngine;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(fileName ="Taunt", menuName ="Game/Skills/Taunt")]
public class Taunt : Skill {

    [Range(0, 100)]
    public int PowerPercentage = 20;
    public TauntedBrain TauntedBrain;
    private TauntedBrain _myClone;

    public override void Initialize(GameObject parent)
    {
        _myClone = Instantiate(TauntedBrain);
        _myClone.Target = parent;
        _action = PerformAction;
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        target.GetComponent<AttackController>().AddBrain(_myClone);
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((Power * PowerPercentage)/100);
        base.PerformAction(actor, target);
    }
}
