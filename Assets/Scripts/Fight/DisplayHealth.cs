#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using Assets.LogicSystem;


public class DisplayHealth : MonoBehaviour
{
    [SerializeField]
    private bool isStatic = false;

    [SerializeField]
    private Image[] HPBars;

    [SerializeField]
    private HealthController[] _controllers = new HealthController[4];

    private Events.MyEvent OnEnterFight, OnBattleWon;

    void Start()
    {
        OnEnterFight = new Events.MyEvent(x =>
        {
            if (!isStatic)
                SetupHPBars(x);
        });
        OnBattleWon = new Events.MyEvent(x =>
        {
            for (int i = 0; i < 4; i++)
                _controllers[i] = null;
        });
        Events.Instance.RegisterForEvent("EnterFight", OnEnterFight);
        Events.Instance.RegisterForEvent("BattleWon", OnBattleWon);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if(_controllers[i] != null )
                HPBars[i].fillAmount = (float)_controllers[i].CurrentHealth / _controllers[i].MaxHealth;
        }
    }

    private void SetupHPBars(object x)
    {
        var group = x as EnemyGroup;
        for (int i = 0; i < 4; i++)
        {
            _controllers[i] = group.enemies[i].GetComponent<HealthController>();
        }
    }
}
