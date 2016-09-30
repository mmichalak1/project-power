using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Brains/RandomBrain")]
public class RandomBrain : AbstractBrain
{
    [HideInInspector]
    public GameObject[] Targets;    
    public int MyDamage;

    private List<GameObject> _checkedTargets = new List<GameObject>(4);
    public override void Initialize(GameObject[] targets)
    {
        Targets = targets;
    }

    public override void Think(GameObject parent)
    {
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
                    Debug.Log("Can't target " + sheep.name + " because is dead.");
                }
            }
            while (true);
            _checkedTargets.Clear();
            if (sheep == null)
            {
                Debug.Log("No valid targets for " + parent.name + ".");
                return;
            }
            IReciveDamage controller = sheep.GetComponent<IReciveDamage>();
            controller.DealDamage(MyDamage, parent);
            Debug.Log(parent.name + " dealt " + MyDamage + " damage to " + sheep.name);
        }

        base.Think(parent);
    }

    private GameObject GetTarget ()
    {
        var sheep = Targets[Random.Range(0, Targets.Length)];
            if (!_checkedTargets.Contains(sheep))
            {
                _checkedTargets.Add(sheep);
                return sheep;
            }

            if ( _checkedTargets.Count == 4)
            {
                return null;
            }

            return GetTarget();
    }
}
