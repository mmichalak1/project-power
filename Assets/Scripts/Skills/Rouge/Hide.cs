using UnityEngine;

[CreateAssetMenu(fileName = "Hide", menuName = "Game/Skills/Hide")]
public class Hide : Skill {

    public int Duration = 2;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var comp = actor.AddComponent<Untargetable>();
        comp.Duration = Duration;
        base.PerformAction(actor, target);
    }
}
