using UnityEngine;
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
        foreach(Skill skill in Skills)
        {
            skill.Initialize();
        }
    }

    public override void Think(GameObject parent)
    {
        //if stunned just end turn
        var state = parent.GetComponent<EntityStatus>();
        if (state.Stunned)
            return;

        var sheep = SelectTarget(parent);
        if (sheep == null)
        {
            Debug.Log("No valid targets for " + parent.name + ".");
            return;
        }
        var skill = Skills.GetRandomElement();
        TurnPlaner.Instance.AddPlan(new Plan(parent, sheep, skill));

        //  Debug.Log(parent.name + " dealt " + _myRealDamage + " damage to " + sheep.name);


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

    private GameObject SelectTarget(GameObject parent)
    {
        //if taunted just take taunt target
        var state = parent.GetComponent<EntityStatus>();
        if(state.Taunted)
        {
            var target = parent.GetComponent<TauntedEffect>().Target;
            if (target.activeSelf)
            {
                if (!target.GetComponent<EntityStatus>().Targetable)
                    return null;
                else
                    return target;
            }
            else
                return null;
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
                    if (!sheep.GetComponent<EntityStatus>().Targetable)
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
            return sheep;
        }
        return null;
    }
}
