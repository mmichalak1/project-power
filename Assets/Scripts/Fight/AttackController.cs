using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class AttackController : MonoBehaviour
{
    public int Damage = 30;
    [SerializeField]
    private AbstractBrain MyBrain;

    [SerializeField]
    private List<AbstractBrain> _brainsList = new List<AbstractBrain>();
    public bool BreakTurn { get; set; }

    // Use this for initialization
    void Start()
    {
        MyBrain.Initialize(GameObject.FindGameObjectsWithTag("Sheep"));
        _brainsList.Add(MyBrain);
    }

    public void PerformAction()
    {
        if (!gameObject.GetComponent<EntityStatus>().Alive)
            return;
        _brainsList = _brainsList.OrderByDescending(x => x.Importance).ToList();
        foreach (var brain in _brainsList.ToArray())
        {
            brain.Think(gameObject);
            if (BreakTurn)
            {
                BreakTurn = false;
                break;
            }
        }
        ClearFinishedBrains();

    }

    private void ClearFinishedBrains()
    {
        var brains = _brainsList.Where(x => x.Duration == 0);
        foreach (var brain in brains)
        {
            if (brain.ParticleEffect != null)
                brain.ParticleEffect.GetComponent<SC_SpellDuration>().enabled = true;
        }
        _brainsList.RemoveAll(x => x.Duration == 0);
    }

    public void AddBrain(AbstractBrain brain)
    {
        _brainsList.Add(brain);
    }

    public void RemoveBrain(AbstractBrain brain)
    {
        _brainsList.Remove(brain);
    }

}