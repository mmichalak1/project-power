using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine.UI;
using Assets.LogicSystem;

public class HealthController : MonoBehaviour, IReciveDamage, ICanBeHealed {

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth = 100;


    void Update()
    {
        if (_currentHealth <= 0)
        {
            Events.Instance.DispatchEvent(gameObject.name + "death", gameObject);
            Debug.Log(gameObject.name + " died.");
            gameObject.SetActive(false);
        }
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public void DealDamage(int value)
    {
        //Debug.Log("Damaging");
        _currentHealth -= value;
    }

    public void Heal(int value)
    {

        //Debug.Log("Healing");
        if (_currentHealth == _maxHealth)
            return;
        else
        {
            if (_currentHealth + value > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth += value;
        }
    }

    public void HealToFull()
    {
        _currentHealth = _maxHealth;
    }
}
