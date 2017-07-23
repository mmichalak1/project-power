using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

[CreateAssetMenu(fileName = "Fireball", menuName = "Game/Skills/Fireball")]
public class Fireball : Skill
{
    [Range(0, 100), Tooltip("Percent of Skill's Power which is dealt to neighbouring enemies (base to target * this value)")]
    public int SplashDamage = 10;

    public override string Description()
    {
        return string.Format(_description, Power, (Power * SplashDamage) / 100);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var enemies = target.transform.parent.gameObject.GetComponent<EnemyGroup>().GetNeighbouringEnemies(target);
        target.GetComponent<IReciveDamage>().DealDamage(Power, actor);
        for (int i = 0; i < enemies.Length; i++)
            if (enemies[i] != null)
                enemies[i].GetComponent<IReciveDamage>().DealDamage((Power * SplashDamage) / 100, actor);
        base.PerformAction(actor, target);
    }
}
