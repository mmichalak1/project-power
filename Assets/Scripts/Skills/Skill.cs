using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Skill : ScriptableObject {

    [SerializeField]
    private int BaseCost = 3;
    [SerializeField]
    private int BaseCooldown = 2;
    [SerializeField]
    private int SkillLevel = 0;
    [SerializeField]
    private bool IsActive = false;

    [SerializeField]
    protected int _power;
    protected System.Action<GameObject, GameObject> _action;
    protected int _cooldown;
    protected int _cost;
    protected GameObject _parent;

    public virtual void Initialize(GameObject parent)
    {
        _cooldown = BaseCooldown;
        _cost = BaseCost;
        _parent = parent;
    }

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
}
