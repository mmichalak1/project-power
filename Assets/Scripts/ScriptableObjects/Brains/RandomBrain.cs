using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using Assets.LogicSystem;

[CreateAssetMenu(menuName = "Game/Brains/RandomBrain")]
public class RandomBrain : AbstractBrain
{
    [HideInInspector]
    public GameObject[] Targets;
    public Skill[] Skills;
    public int MyDamage;


    private List<GameObject> _checkedTargets = new List<GameObject>(4);
    public override void Initialize(GameObject[] targets)
    {
        Targets = targets;
    }

    public override void Think(GameObject parent)
    {
        var _myRealDamage = parent.GetComponent<AttackController>().Damage;
        var debuffs = parent.GetComponents<DamageDebuff>();

        if (debuffs.Length != 0)
        {
            foreach (var debuff in debuffs)
            {
                _myRealDamage -= (_myRealDamage * debuff.DebuffValue) / 100;
            }
        }

        List<GameObject> availableTargets = new List<GameObject>();
        foreach (GameObject x in Targets)
        {
            if (x != null)
                availableTargets.Add(x);
        }
        if (availableTargets.Count > 0)
        {
            GameObject sheep;
            do
            {
                sheep = GetTarget();
                if (sheep == null)
                    break;
                if (sheep.activeSelf)
                {
                    if (sheep.GetComponent<Untargetable>() != null)
                        Debug.Log(sheep.name + " is untargetable");
                    else
                        break;
                }
                else
                {
                    Debug.Log("Can't target " + sheep.name + " because it's dead.");
                }
            }
            while (true);
            _checkedTargets.Clear();
            if (sheep == null)
            {
                Debug.Log("No valid targets for " + parent.name + ".");
                return;
            }
            var skill = GetSkill();
            skill.Initialize();
            skill.Power = _myRealDamage;
            TurnPlaner.Instance.AddPlan(parent.name, new Plan(parent, sheep, skill));

            //  Debug.Log(parent.name + " dealt " + _myRealDamage + " damage to " + sheep.name);
        }

        base.Think(parent);
    }

    private GameObject GetTarget()
    {
        var sheep = Targets[Random.Range(0, Targets.Length)];
        if (!_checkedTargets.Contains(sheep))
        {
            _checkedTargets.Add(sheep);
            return sheep;
        }

        if (_checkedTargets.Count == 4)
        {
            return null;
        }

        return GetTarget();
    }

    private Skill GetSkill()
    {
        return Instantiate(Skills[Random.Range(0, Skills.Length - 1)]);
    }
}
