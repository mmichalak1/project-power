using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Poison", menuName = "Game/Skills/Poison")]
public class Poison : Skill {

    [Range(100, 300)]
    public int DamageMultiplier;

    public Assets.Scripts.ScriptableObjects.PoisonedBrain PoisonedBrain;
    private Assets.Scripts.ScriptableObjects.PoisonedBrain _poisonedBrainCopy;

    public override void Initialize(GameObject parent)
    {
        _poisonedBrainCopy = Instantiate(PoisonedBrain);
        _poisonedBrainCopy.Damage = Power * (DamageMultiplier / 100);

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
