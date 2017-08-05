using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using Assets.LogicSystem;

public class HealthController : MonoBehaviour, IReciveDamage, ICanBeHealed
{

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth = 100;
    [SerializeField]
    private float _defence = 0;
    [SerializeField]
    GameObject model;

    public float Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }
    public bool IsAlive { get { return _currentHealth > 0; } }

    public GameObject LastAttacker = null;

    public DamageIndicatorScript DamageIndicator;

    public Image HealthIndicator;

    public Text HealthText;

    void Start()
    {
        UpdateHealthBar();
    }

    void Update()
    {
        if (_currentHealth <= 0)
        {
            Events.Instance.DispatchEvent(gameObject.name + "death", gameObject);
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

    /// <summary>
    /// Use when want to calculate for damage returningm taunting and so on
    /// </summary>
    /// <param name="value"> Damage to deal</param>
    /// <param name="source"> Source of the damage</param>
    public void DealDamage(int value, GameObject source)
    {
        if (CurrentHealth <= 0)
            return;
        LastAttacker = source;
        var redirectComponent = gameObject.GetComponent<RedirectDamage>();
        if (redirectComponent != null)
        {
            Debug.Log("Damage to " + gameObject.name + " redirected to " + redirectComponent.newTarget.name + ".");
            if (redirectComponent.newTarget != gameObject && redirectComponent.newTarget.activeSelf)
            {
                redirectComponent.newTarget.GetComponent<IReciveDamage>().DealDamage(value, source);
                return;
            }
        }

        var fightBackComp = gameObject.GetComponent<GiveAwayDamage>();
        if (fightBackComp != null)
            fightBackComp.FightBack(value, source);

        var damageReductor = gameObject.GetComponent<DamageReductor>();
        if (damageReductor != null)
        {
            Debug.Log("Damage reduced from " + value + " by " + damageReductor.DamageReduced + "%");
            value = (value * damageReductor.DamageReduced) / 100;
        }




        DealDamage(value);
    }

    ///<summary>
    ///Ignores special states like redirection or damage returning
    ///</summary>
    public void DealDamage(int value)
    {
        //Debug.Log("Defence: " + _defence);
        if (_defence > 0)
            value -= (int)(value * _defence / 100.0f);
        _currentHealth -= value;
        if (DamageIndicator != null)
        {
            DamageIndicator.BeginIndication(0 - value);
        }
        UpdateHealthBar();
    }

    public void Heal(int value)
    {

        //Debug.Log("Healing");
        if (_currentHealth == _maxHealth || _currentHealth <= 0)
            return;
        else
        {
            if (_currentHealth + value > _maxHealth)
            {
                int difference = _maxHealth - _currentHealth;
                _currentHealth = _maxHealth;
                if (DamageIndicator != null)
                    DamageIndicator.BeginIndication(difference);

            }
            else
            {
                _currentHealth += value;
                if (DamageIndicator != null)
                    DamageIndicator.BeginIndication(value);
            }
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        if (HealthIndicator != null)
            HealthIndicator.fillAmount = (float)_currentHealth / (float)_maxHealth;
        if (HealthText != null)
            HealthText.text = _currentHealth + "/" + _maxHealth;
    }

    public void HealToFull()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
}
