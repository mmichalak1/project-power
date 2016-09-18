using UnityEngine;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(fileName ="Taunt", menuName ="Game/Skills/Taunt")]
public class Taunt : Skill {

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
        base.PerformAction(actor, target);
    }
}
