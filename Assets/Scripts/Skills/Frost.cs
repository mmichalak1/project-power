using UnityEngine;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(fileName = "Frost", menuName = "Game/Skills/Frost")]
public class Frost : Skill {

    public int StunDuration = 2;
    public StunnedBrain StunnedBrain;
    private StunnedBrain _stunnedBrainCopy;

    public override void Initialize(GameObject parent)
    {
        _stunnedBrainCopy = Instantiate(StunnedBrain);
        _stunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " stuns " + target.name + " for " + (StunDuration) + " for turns.");
        target.GetComponent<AttackController>().AddBrain(_stunnedBrainCopy);
        base.PerformAction(actor, target);
    }

}
