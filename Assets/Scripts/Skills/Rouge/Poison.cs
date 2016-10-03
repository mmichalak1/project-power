using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Poison", menuName = "Game/Skills/Poison")]
public class Poison : Skill {

    public Assets.Scripts.ScriptableObjects.PoisonedBrain PoisonedBrain;
    private Assets.Scripts.ScriptableObjects.PoisonedBrain _poisonedBrainCopy;

    public override void Initialize(GameObject parent)
    {
        _poisonedBrainCopy = Instantiate(PoisonedBrain);
        _poisonedBrainCopy.Damage = Power;

        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var attackComponent = target.GetComponent<AttackController>();
        if (null == attackComponent)
            return;
        attackComponent.AddBrain(_poisonedBrainCopy);
        base.PerformAction(actor, target);
    }
}
