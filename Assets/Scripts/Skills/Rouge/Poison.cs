using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Game/Skills/Poison")]
public class Poison : Skill {

    public Assets.Scripts.ScriptableObjects.PoisonedBrain PoisonedBrain;
    private Assets.Scripts.ScriptableObjects.PoisonedBrain _poisonedBrainCopy;

    public override string Description()
    {
        return string.Format(_description, PoisonedBrain.Duration, Power);
    }

    public override void Initialize(GameObject parent)
    {
        _poisonedBrainCopy = Instantiate(PoisonedBrain);
        _poisonedBrainCopy.Damage = Power;
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        _poisonedBrainCopy = Instantiate(PoisonedBrain);
        _poisonedBrainCopy.Damage = Power;
        base.Initialize(data);
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
