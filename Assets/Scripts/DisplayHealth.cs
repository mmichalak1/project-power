using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField]
    private bool isStatic = false;

    [SerializeField]
    private Image[] HPBars;

    [SerializeField]
    private HealthController[] _controllers = new HealthController[4];


    void Start()
    {
        if (isStatic)
            return;
        Assets.LogicSystem.Events.Instance.RegisterForEvent("EnterFight", x =>
        {
            SetupHPBars(x);
        });

        Assets.LogicSystem.Events.Instance.RegisterForEvent("BattleWon", x =>
        {
            for (int i = 0; i < 4; i++)
                _controllers[i] = null;
        });
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
        var group = x as WolfGroupManager;
        for (int i = 0; i < 4; i++)
        {
            _controllers[i] = group.enemies[i].GetComponent<HealthController>();
        }
    }
}
