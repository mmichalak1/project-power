using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "Banana Throw", menuName = "Game/Skills/Banana Throw")]
public class BananaThrow : Skill {

    public int StunDuration = 1;
    public StunnedBrain StunnedBrain;
    private StunnedBrain _myStunnedBrainCopy;
    public GameObject ParticleEffect;

    public override string Description()
    {
        return string.Format(_description, StunDuration);
    }

    // Use this for initialization
    public override void Initialize(GameObject parent)
    {
        _myStunnedBrainCopy = Instantiate(StunnedBrain);
        _myStunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(parent);
    }

    public override void Initialize(EntityData data)
    {
        _myStunnedBrainCopy = Instantiate(StunnedBrain);
        _myStunnedBrainCopy.SetDuration(StunDuration);
        base.Initialize(data);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        _myStunnedBrainCopy.ParticleEffect = go;
        Debug.Log(actor.name + " stuns " + target.name + "for " + StunDuration + " turns.");
        target.GetComponent<AttackController>().AddBrain(_myStunnedBrainCopy);
        base.PerformAction(actor, target);
    }



}
