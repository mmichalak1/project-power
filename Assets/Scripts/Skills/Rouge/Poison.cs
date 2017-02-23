using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Game/Skills/Poison")]
public class Poison : Skill
{

    public Assets.Scripts.ScriptableObjects.PoisonedBrain PoisonedBrain;
    private Assets.Scripts.ScriptableObjects.PoisonedBrain _poisonedBrainCopy;
    public GameObject ParticleEffect;
    public TargetOffset tOffset;

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
        Vector3 targetOffset = Vector3.zero;
        var targetingOffset = target.GetComponent<TargetingOffset>();
        if (targetingOffset != null)
        {
            switch (tOffset)
            {
                case TargetOffset.Belly: { targetOffset = targetingOffset.Belly; } break;
                case TargetOffset.Head: { targetOffset = targetingOffset.Head; } break;
            }
        }
        GameObject go = Instantiate(ParticleEffect, target.transform.position + targetOffset, Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        _poisonedBrainCopy.ParticleEffect = go;
        var attackComponent = target.GetComponent<AttackController>();
        if (null == attackComponent)
            return;
        attackComponent.AddBrain(_poisonedBrainCopy);
        base.PerformAction(actor, target);
    }
}
