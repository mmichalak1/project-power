﻿using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

    private static Dictionary<string, string> ColorCoding = new Dictionary<string, string>()
    {
        { "<damage>", "<color=\"red\">" },
        { "</damage>", "</color>" },
        { "<stun>", "<color=\"cyan\">" },
        { "</stun>", "</color>" },
        { "<heal>", "<color=\"lime\">" },
        { "</heal>", "</color>" }
    };

    protected static string ProcessString(string input)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder(input);
        foreach (var item in ColorCoding)
        {
            builder.Replace(item.Key, item.Value);
        }
        return builder.ToString();
    }


    [SerializeField, Tooltip("Basic percentage of power's value calculated from entity's stats."), Range(0, 500)]
    private int StatsMultiplier = 70;
    [SerializeField, Tooltip("Skill's cost.")]
    private int BaseCost = 3;
    [SerializeField, Tooltip("Skill's cooldown, when declaring always add +1 to it. Example: if you want 1 turn cooldown write 2.")]
    private int BaseCooldown = 2;
    [SerializeField]
    public int SkillLevel = 0;
    [SerializeField, Tooltip("Defines if skill was learned by given sheep")]
    private bool isActive = false;
    [SerializeField, Tooltip("Cost of unlocking and upgrading skill")]
    private int unlockCost = 20;
    [SerializeField, TextArea(3, 5)]
    private string _rawDescription = "This is Skill's Description";
    [SerializeField]
    private bool isBasicSkill = false;
    [SerializeField]
    private PossibleTarget _skillTarget;
    public int _requiredSheepLevel = 5;
    public Effect OnCastEffect;
    public Effect OnHitEffect;


    protected string _description
    {
        get { return ProcessString(_rawDescription); }
    }
    public Skill[] RequiredSkills;

    protected System.Action<GameObject, GameObject> _action;
    protected int _cooldown;
    protected int _cost;
    protected IProvideStatistics statisticsProvider;


    public Sprite Icon;
    public string Name
    {
        get { return name; }
    }
    public System.Action<GameObject, GameObject> Action
    {
        get { return _action; }
    }

    public int Cost
    {
        get { return _cost; }
    }

    public int Cooldown
    {
        get { return _cooldown; }
    }
    public int CooldownBase
    {
        get { return BaseCooldown; }
    }

    public int ResourceCost
    {
        get { return BaseCost; }
    }

    protected int _baseCooldown
    {
        get { return BaseCooldown; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set
        {
            if (isBasicSkill)
            {
                isActive = true;
                return;
            }
            isActive = value;
        }
    }

    public int UnlockCost
    {
        get { return unlockCost; }
    }

    public int RequiredSheepLevel
    {
        get { return _requiredSheepLevel; }
    }

    public PossibleTarget SkillTarget
    {
        get { return _skillTarget; }
    }

    public int Power
    {
        get
        {
            int baseDmg =statisticsProvider.GetDamage();
            return (baseDmg * StatsMultiplier) / 100;
        }
    }


    public abstract string Description();
    protected virtual void PerformAction(GameObject actor, GameObject target)
    {
        _cooldown = BaseCooldown;
    }
    public virtual void OnSkillPlanned(GameObject actor, GameObject target)
    {

    }
    public virtual void Initialize(IProvideStatistics statProvider)
    {
        _cooldown = 0;
        _cost = BaseCost;
        statisticsProvider = statProvider;
        _action = PerformAction;
    }

    /// <summary>
    /// Initializes with only adding Perform Action to _action
    /// </summary>
    public virtual void Initialize()
    {
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
    public bool IsTargetValid(GameObject source, GameObject target)
    {
        var sourceAffiliation = source.GetComponent<EntityAffiliation>().Affiliation;
        var targetAffiliation = target.GetComponent<EntityAffiliation>().Affiliation;

        //when skill target must be friendly both affiliations must be same, if hostile they must be diffrent
        if (_skillTarget == PossibleTarget.Friendly && sourceAffiliation == targetAffiliation)
        {
            return true;
        }
        
        if(_skillTarget == PossibleTarget.Hostile && sourceAffiliation != targetAffiliation)
        {
            return true;
        }

        return false;
    }
}
