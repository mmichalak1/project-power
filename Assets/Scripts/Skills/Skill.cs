using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject {

    private static System.Collections.Generic.Dictionary<string, string> ColorCoding = new System.Collections.Generic.Dictionary<string, string>()
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


    [SerializeField, Tooltip("Basic percentage of power's value calculated from sheep's stats."), Range(0, 500)]
    private int StatsMultiplier = 70;
    [SerializeField, Tooltip("Skill's cost.")]
    private int BaseCost = 3;
    [SerializeField, Tooltip("Skill's cooldown, when declaring always add +1 to it. Example: if you want 1 turn cooldown write 2.")]
    private int BaseCooldown = 2;
    [SerializeField]
    private int SkillLevel = 0;
    [SerializeField, Tooltip("Defines if skill was learned by given sheep")]
    private bool isActive = false;
    [SerializeField, Tooltip("Cost of unlocking and upgrading skill")]
    private int unlockCost = 20;
    [SerializeField, TextArea(3,5)]
    private string _rawDescription = "This is Skill's Description";
    [SerializeField]
    private bool isBasicSkill = false;
    public int _requiredSheepLevel = 5;
    public float effectDuration = 1;
    public Effect OnCastEffect;
    public Effect OnHitEffect;


    protected string _description
    {
        get { return ProcessString(_rawDescription); }
    }
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
        set { _power = value; }
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


    public abstract string Description();


    protected virtual void PerformAction(GameObject actor, GameObject target)
    {
        _cooldown = BaseCooldown;
    }

    public virtual void OnSkillPlanned(GameObject actor, GameObject target)
    {

    }
    public virtual void Initialize(GameObject parent)
    {
        _cooldown = 0;
        _cost = BaseCost;
        _power = (parent.GetComponent<EntityDataHolder>().SheepData.TotalAttack * StatsMultiplier)/100;
        _parent = parent;
        _action = PerformAction;
    }

    /// <summary>
    /// Initializes with only adding Perform Action to _action
    /// </summary>
    public virtual void Initialize()
    {
        _action = PerformAction;
    }

    public virtual void Initialize(EntityData data)
    {
        _power = (data.TotalAttack * StatsMultiplier) / 100;
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
