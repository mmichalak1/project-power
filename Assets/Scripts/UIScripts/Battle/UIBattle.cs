using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class UIBattle : MonoBehaviour
{
    public UIBattleActionQueuePanel ActionQueue;
    public UIBattleResourcePanel ResourcePanel;
    public UIBattleSkillPanel SkillPanel;

    void Awake()
    {
        Events.Instance.RegisterForEvent("activateBattleUI", Activate);
    }

    public void Activate(object obj)
    {
        ActionQueue.Activate(true);
        ResourcePanel.Activate(true);
        SkillPanel.Activate(true);
    }

}
