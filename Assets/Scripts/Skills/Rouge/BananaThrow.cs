using UnityEngine;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(fileName = "Banana Throw", menuName = "Game/Skills/Banana Throw")]
public class BananaThrow : Skill {

    public int StunDuration = 1;
    public StunnedBrain StunnedBrain;
    private StunnedBrain _myStunnedBrainCopy;
    // Use this for initialization
    public override void Initialize(GameObject parent)
    {
        _myStunnedBrainCopy = Instantiate(StunnedBrain);
        _myStunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log(actor.name + " stuns " + target.name + "for " + StunDuration + " turns.");
        target.GetComponent<AttackController>().AddBrain(_myStunnedBrainCopy);
        base.PerformAction(actor, target);
    }



}
