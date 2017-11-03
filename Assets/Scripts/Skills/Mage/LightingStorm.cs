using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Interfaces;

[CreateAssetMenu(fileName = "Lighting Storm", menuName = "Game/Skills/Lighting Storm")]
public class LightingStorm : Skill, NotTargetableSkill {

    public GameObject ParticleEffect;

    public override string Description()
    {
        return string.Format(_description, Power);
    }

    public GameObject GetTarget(SheepGroupManager sheep, EnemyGroup enemies)
    {
        return enemies.enemies[0];
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        Debug.Log("The storm is real! Every enemy takes " + Power + " damage!");
        var group = target.transform.parent.gameObject.GetComponent<EnemyGroup>().enemies;
        foreach (var enemy in group)
        {
            enemy.gameObject.GetComponent<IReciveDamage>().DealDamage(Power, actor);
        }
        base.PerformAction(actor, target);
    }}

