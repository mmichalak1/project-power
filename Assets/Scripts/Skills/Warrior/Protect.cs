using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Redirect Damage", menuName = "Game/Skills/Redirect Damage")]
public class Protect : Skill {

    [Range(1, 5)]
    public int Duration; 

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    public override void OnSkillPlanned(GameObject actor, GameObject target)
    {
        var comp = target.AddComponent<RedirectDamage>();
        _cooldown = _baseCooldown;
        comp.newTarget = actor;
        comp.RedirectCounter = Duration;
        base.OnSkillPlanned(actor, target);
    }
}
