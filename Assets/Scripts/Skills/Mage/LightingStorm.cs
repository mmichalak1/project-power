using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Lighting Storm", menuName = "Game/Skills/Lighting Storm")]
public class LightingStorm : Skill {

    [Range(10, 100)]
    public int DamageMultiplier = 50;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log("The storm is real! Every enemy takes " + (Power * DamageMultiplier) / 100 + " damage!");
        var group = target.transform.parent.gameObject.GetComponent<WolfGroupManager>().enemies;
        foreach (var enemy in group)
        {
            enemy.gameObject.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((Power * DamageMultiplier) / 100, actor);
        }
        base.PerformAction(actor, target);
    }}

