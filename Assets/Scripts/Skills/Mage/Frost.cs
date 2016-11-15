using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Frost", menuName = "Game/Skills/Frost")]
public class Frost : Skill
{

    public int StunDuration = 2;
    public StunnedBrain StunnedBrain;
    private StunnedBrain _stunnedBrainCopy;

    public override string Description()
    {
        return string.Format(_description, StunDuration);
    }

    public override void Initialize(GameObject parent)
    {
        _stunnedBrainCopy = Instantiate(StunnedBrain);
        _stunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        _stunnedBrainCopy = Instantiate(StunnedBrain);
        _stunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(data);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var attack = target.GetComponent<AttackController>();
        if (attack == null)
            return;
        Debug.Log(actor.name + " stuns " + target.name + " for " + (StunDuration) + " for turns.");
        attack.AddBrain(_stunnedBrainCopy);
        base.PerformAction(actor, target);
    }

}
