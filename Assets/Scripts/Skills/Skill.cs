﻿using UnityEngine;

public abstract class Skill : ScriptableObject {

    [SerializeField, Tooltip("Damage dealt by attack, shield power, heal power.")]
    private int BasePower = 60;
    [SerializeField, Tooltip("Skill's cost.")]
    private int BaseCost = 3;
    [SerializeField, Tooltip("Skill's cooldown, when declaring always add +1 to it. Example: if you want 1 turn cooldown write 2.")]
    private int BaseCooldown = 2;
    [SerializeField]
    private int SkillLevel = 0;
    [SerializeField, Tooltip("Defines if skill was learned by given sheep")]
    private bool IsActive = false;

    public Skill[] RequiredSkills;

    protected int _power;
    protected System.Action<GameObject, GameObject> _action;
    protected int _cooldown;
    protected int _cost;
    protected GameObject _parent;

    public Sprite Icon;
    public string Name
    {
        get { return name; }
    }
    [HideInInspector]
    public System.Action<GameObject, GameObject> Action
    {
        get { return _action; }
    }
    /// <summary>
    /// Defines shield's power, amount hp healed, dmg dealt by skill
    /// </summary>
    public int Power
    {
        get { return _power; }
    }

    public int Cost
    {
        get { return _cost; }
    }

    public int Cooldown
    {
        get { return _cooldown; }
    }


    protected virtual void PerformAction(GameObject actor, GameObject target)
    {
        _cooldown = BaseCooldown;
    }
    public virtual void Initialize(GameObject parent)
    {
        _cooldown = 0;
        _cost = BaseCost;
        _power = BasePower;
        _parent = parent;
        _action = PerformAction;
    }

    public void UpdateCooldown()
    {
        _cooldown--;
    }

    public void ResetCooldown()
    {
        _cooldown = 0;
    }
}
