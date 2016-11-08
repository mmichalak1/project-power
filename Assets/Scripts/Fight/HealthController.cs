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
    private int _defence = 0;

    public int Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }
        

    public GameObject LastAttacker = null;

    public DamageIndicatorScript DamageIndicator;

    public Image HealthIndicator;


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
        //Debug.Log("Damaging");
        var redirectComponent = gameObject.GetComponent<RedirectDamage>();
        if (redirectComponent != null)
        {
            Debug.Log("Damage to " + gameObject.name + " redirected to " + redirectComponent.newTarget.name + ".");
            redirectComponent.newTarget.GetComponent<IReciveDamage>().DealDamage(value, source);
            return;
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
        if (_defence >= 60)
            value *= 60 / 1000;
        else
            value *= _defence / 1000;
        _currentHealth -= value;
        if (DamageIndicator != null)
            DamageIndicator.BeginIndication(0 - value);
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
                _currentHealth = _maxHealth;
                if (DamageIndicator != null)
                    DamageIndicator.BeginIndication(_maxHealth - _currentHealth);

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
    }

    public void HealToFull()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
}
