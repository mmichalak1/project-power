﻿using UnityEngine;
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
        Events.Instance.RegisterForEvent("disableBattleUI", Deactivate);
    }

    public void Activate(object obj)
    {
        ActionQueue.Activate(true);
        ResourcePanel.Activate(true);
        SkillPanel.Activate(true);
    }

    public void Deactivate(object obj)
    {
        ActionQueue.Activate(false);
        ResourcePanel.Activate(false);
        SkillPanel.Activate(false);
    }

}
