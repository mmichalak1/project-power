using UnityEngine;
using Assets.Scripts.Interfaces;

[CreateAssetMenu(fileName = "Fireball", menuName = "Game/Skills/Fireball")]
public class Fireball : Skill
{

    [Range(0, 100), Tooltip("Percent of Skill's Power which is dealt to neighbouring enemies")]
    public int SplashDamage = 10;

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var enemies = target.transform.parent.gameObject.GetComponent<WolfGroupManager>().GetNeighbouringEnemies(target);
        target.GetComponent<IReciveDamage>().DealDamage(Power, actor);
        for (int i = 0; i < enemies.Length; i++)
            if (enemies[i] != null)
                enemies[i].GetComponent<IReciveDamage>().DealDamage((Power * SplashDamage) / 100, actor);
        base.PerformAction(actor, target);
    }
}
