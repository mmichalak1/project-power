using UnityEngine;

[CreateAssetMenu(fileName = "Redirect Damage", menuName = "Game/Skills/Redirect Damage")]
public class Protect : Skill {

    [Range(1, 5)]
    public int Duration;
    public GameObject ParticleEffect;

    public override string Description()
    {
        return string.Format(_description, Duration);
    }

    public override void OnSkillPlanned(GameObject actor, GameObject target)
    {
        GameObject go = Instantiate(ParticleEffect, target.transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
        go.transform.parent = target.transform;
        var comp = target.AddComponent<RedirectDamage>();
        _cooldown = _baseCooldown;
        comp.ParticleEffect = go;
        comp.newTarget = actor;
        comp.RedirectCounter = Duration;
        base.OnSkillPlanned(actor, target);
    }
}
