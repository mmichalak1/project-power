using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IReciveDamage, ICanBeHealed {

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth = 100;


    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    void Update()
    {
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Debug.Log("hit: " + gameObject.name);
        if (Input.GetKey(KeyCode.LeftControl))
            DealDamage(10);
        else
            Heal(7);
    }

    public void DealDamage(int value)
    {
        _currentHealth -= value;
    }

    public void Heal(int value)
    {
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
}
