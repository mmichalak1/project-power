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
            var sheep = availableTargets[Random.Range(0, availableTargets.Count - 1)];
            IReciveDamage controller = sheep.GetComponent<IReciveDamage>();
            controller.DealDamage(MyDamage);
            Debug.Log(parent.name + " dealt " + MyDamage + " damage to " + sheep.name);
        }

        base.Think(parent);
    }
}
