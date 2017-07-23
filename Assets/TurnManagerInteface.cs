using UnityEngine;
using Assets.Scripts.Interfaces;
using System;
using Assets.LogicSystem;

public class TurnManagerInteface : MonoBehaviour, ISystem {

    #region References To Ohter Systems
    public EntitySelector selector;
    public UIBattleSkillPanel skillPanel;
    public BattleResourcesController resourcesController;
    public CancelButtonScript cancelButton;
    public TurnQueueController queueController;
    #endregion

    #region Unity Hooks
    private void Awake()
    {
        SystemAccessor.AddSystem(this);
    }

    private void OnDestroy()
    {
        SystemAccessor.RemoveSystem(this);
    }
    #endregion

    public GameObject SelectedSheep { get; private set; }
    public Skill SelectedSkill { get; private set; }
    public EnemyGroup CurrentGroup { get; private set; }



    #region Selection Hooks
    public void SelectSheep(GameObject sheep)
    {
        if (SelectedSheep != null)
            SelectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(false);
        skillPanel.LoadSkillsData(sheep.GetComponent<EntityDataHolder>().SheepData.SheepSkills);
        sheep.transform.FindChild("SelectRing").gameObject.SetActive(true);
        SelectedSheep = sheep;
    }
    public void SelectSkill(Skill skill)
    {
        if (skill == null)
            return;

        if (TurnPlaner.Instance.ContainsPlanWithSkill(skill))
            return;

        if (!resourcesController.TryAllocateResources(skill.Cost))
            return;
        SelectedSkill = skill;
        selector.StartSearching(SelectTarget, new Func<GameObject, bool>(x => x.CompareTag("Sheep") || x.CompareTag("Enemy")));
        cancelButton.Show();
    }
    public void SelectTarget(GameObject target)
    {
        //If target not valid drop action
        if (SelectedSkill == null || !SelectedSkill.IsTargetValid(SelectedSheep, target))
            return;

        Plan plan = new Plan(SelectedSheep, target.transform.gameObject, SelectedSkill);

        //If plan with given skill exists cancel it
        if (TurnPlaner.Instance.ContainsPlan(plan))
        {
            resourcesController.TakeResources(-plan.Skill.Cost);
            queueController.RemovePlan(plan);
            TurnPlaner.Instance.CancelPlan(plan);
        }
            
        //Remove resources for skill
        resourcesController.TakeResources(SelectedSkill.Cost);

        //Add plan to planner and queue
        TurnPlaner.Instance.AddPlan(plan);
        queueController.AddPlan(plan);

        //Call OnSkillPlanned
        SelectedSkill.OnSkillPlanned(SelectedSheep, target);

        //Hide Button and SelectedSkill
        Clear();
    }
    #endregion


    public void BeginFight(EnemyGroup group)
    {
        CurrentGroup = group;
        selector.StartSearching(SelectSheep, new Func<GameObject, bool>(x => x.CompareTag("Sheep") && x != SelectedSheep));
    }

    
    

    public void EndTurn()
    {

    }

    public void CancelSkill()
    {
        SelectedSkill = null;
        selector.StartSearching(SelectSheep, new Func<GameObject, bool>(x => x.CompareTag("Sheep") && x != SelectedSheep));
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        cancelButton.Hide();
    }


    public void Clear()
    {
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        SelectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(false);
        cancelButton.Hide();
        SelectedSheep = null;
        SelectedSkill = null;
    }


}
