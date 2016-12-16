using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu (fileName = "Damage Debuff", menuName = "Game/Skills/Damage Debuff")]
public class Debuff : Skill {

    [Range(1,5)]
    public int Duration = 2;
    [Range(0, 70)]
    public int DebuffValue = 20;
    public GameObject ParticleEffect;

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        var debuff = target.AddComponent<DamageDebuff>();
        debuff.ParticleEffect = go;
        debuff.Duration = Duration;
        debuff.DebuffValue = DebuffValue;
        base.PerformAction(actor, target);
    }

    public override string Description()
    {
        return string.Format(_description, Duration, DebuffValue);
    }
}
