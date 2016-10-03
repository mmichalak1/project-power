using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Lighting Storm", menuName = "Game/Skills/Lighting Storm")]
public class LightingStorm : Skill {

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log("The storm is real! Every enemy takes " + Power + " damage!");
        var group = target.transform.parent.gameObject.GetComponent<WolfGroupManager>().enemies;
        foreach (var enemy in group)
        {
            enemy.gameObject.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(Power, actor);
        }
        base.PerformAction(actor, target);
    }}

