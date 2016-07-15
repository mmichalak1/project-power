using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.ScriptableObjects;

[CreateAssetMenu(menuName = "Brains/RandomBrain")]
public class RandomBrain : AbstractBrain {

    public GameObject[] Targets;

    public override void Initialize(GameObject[] targets)
    {
        Targets = targets;
    }

    public override void Think(GameObject parent)
    {
        var sheep = Targets[Random.Range(0, Targets.Length)];
        IReciveDamage controller = sheep.GetComponent<IReciveDamage>();
        controller.DealDamage(5);
        Debug.Log(parent.name + " dealt 5 damage to " + sheep.name);
    }
}
