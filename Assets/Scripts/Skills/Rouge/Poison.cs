using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Game/Skills/Poison")]
public class Poison : Skill
{

    public GameObject ParticleEffect;
    public TargetOffset tOffset;

    public int PoisonDuration;

    public override string Description()
    {
        return string.Format(_description, PoisonDuration, Power);
    }

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
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
        var poisonEffect = target.AddComponent<PoisonEffect>();
        poisonEffect.ParticleEffect = go;
        poisonEffect.Damage = Power;
        poisonEffect.Duration = PoisonDuration;
        poisonEffect.Source = actor;
        base.PerformAction(actor, target);
    }
}
