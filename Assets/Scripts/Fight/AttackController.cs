using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using System;

public class AttackController : MonoBehaviour, IProvideStatistics
{
    [SerializeField]
    private int Damage = 30;
    [SerializeField]
    private int Defence = 0;
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

    public int GetDamage()
    {
        return Damage;
    }

    public int GetDefence()
    {
        return Defence;
    }

    public int GetMaxHealth()
    {
        return GetComponent<HealthController>().MaxHealth;
    }

    public float GetDamageMultiplicator()
    {
        return 1.0f;
    }
}