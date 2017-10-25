using UnityEngine;
using Assets.Scripts.Interfaces;
using System;
using Assets.LogicSystem;

public class TurnManager : MonoBehaviour, ISystem, ITurnManager
{


    #region References To Other Systems
    public EntitySelector selector;
    public UIBattleSkillPanel skillPanel;
    public BattleResourcesController resourcesController;
    public CancelButtonScript cancelButton;
    public TurnQueueController queueController;
    public GameObject endTurnConfirmation;
    public TurnPlayer turnPlayer;
    public AfterBattleScreen afterBattleInterface;
    #endregion

    #region UI Canvases
    public GameObject battleUI;
    public GameObject explorationUI;
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

    private GameObject selectedSheep = null;

    public GameObject SelectedSheep
    {
        get { return selectedSheep; }
        private set
        {
            if (selectedSheep != null)
                selectedSheep.transform.Find("SelectRing").gameObject.SetActive(false);
            selectedSheep = value;
            if (selectedSheep != null)
                selectedSheep.transform.Find("SelectRing").gameObject.SetActive(true);
        }
    }
    public Skill SelectedSkill { get; private set; }
    public EnemyGroup EnemyGroup { get; private set; }
    public SheepGroupManager SheepGroup { get; set; }


    #region Selection Hooks
    public void SelectSheep(GameObject sheep)
    {
        skillPanel.LoadSkillsData(sheep.GetComponent<EntityDataHolder>().SheepData.SheepSkills);
        SelectedSheep = sheep;
    }
    public bool SelectSkill(Skill skill)
    {
        if (skill == null || selectedSheep == null)
            return false;

        if (TurnPlaner.Instance.ContainsPlanWithSkill(skill))
            return false;

        if (!resourcesController.MoveToBuffer(skill.Cost))
            return false;
        SelectedSkill = skill;
        selector.StartSearching(SelectTarget, CanEntityBeTarget);
        cancelButton.Show();
        return true;
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
            resourcesController.MoveFromTakenToAvailable(plan.Skill.Cost);
            queueController.RemovePlan(plan);
            TurnPlaner.Instance.CancelPlan(plan);
        }

        //Remove resources for skill
        resourcesController.MoveFromBufferToTaken();

        //Add plan to planner and queue
        TurnPlaner.Instance.AddPlan(plan);
        queueController.AddPlan(plan);

        //Call OnSkillPlanned
        SelectedSkill.OnSkillPlanned(SelectedSheep, target);

        //Hide Button and SelectedSkill
        cancelButton.Hide();
        SelectedSkill = null;

        //Start serach for new sheep to act
        selector.StartSearching(SelectSheep, CanSheepBeSelected);

        //Hide selected skill
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);

    }
    #endregion
    public void BeginFight(EnemyGroup group)
    {
        EnemyGroup = group;
        BeginTurn();
    }
    public void EndTurn(bool forced)
    {
        //forced is varaible which is set to true when player knows he didnt spent all resources and still wants to end turn
        if (!forced && resourcesController.FullResources)
        {
            endTurnConfirmation.gameObject.SetActive(true);
            return;
        }

        SelectedSheep = null;
        //ignore sheep selection
        selector.StartSearching(SelectSheep, new Func<GameObject, bool>(x => false));


        //Order TurnPlayer to start playing every move and pass function for wolves thinking
        turnPlayer.PlayTurn(PostTurnActions, () =>
            {
                if (EnemyGroup != null)
                    EnemyGroup.GetComponent<EnemyGroup>().ApplyGroupTurn();
            }
            );
    }
    private void BeginTurn()
    {
        //Play all effects at TurnStart
        foreach (var go in SheepGroup.Sheep)
            foreach (var effect in go.GetComponents<IOnTurnBegin>())
                effect.OnTurnBegin();

        foreach (var go in EnemyGroup.enemies)
            foreach (var effect in go.GetComponents<IOnTurnBegin>())
                effect.OnTurnBegin();

        //Start selecting sheep
        selector.StartSearching(SelectSheep, CanSheepBeSelected);
    }
    private void PostTurnActions()
    {
        //Tick all cooldowns for sheep
        foreach (var obj in SheepGroup.Sheep)
            obj.GetComponent<EntityDataHolder>().SheepData.SheepSkills.UpdateCooldowns();

        //Vi won 
        if (EnemyGroup.IsGroupFinished())
        {
            var effects = FindObjectsOfType<SC_SpellDuration>();
            foreach (var effect in effects)
            {
                effect.enabled = true;
            }
            battleUI.SetActive(false);
            explorationUI.SetActive(true);
            afterBattleInterface.Show(EnemyGroup.WoolForFight);
            Events.Instance.DispatchEvent("BattleWon", null);
            Events.Instance.DispatchEvent("disableBattleUI", null);
            Destroy(EnemyGroup.gameObject);
            EnemyGroup = null;

        }
        else
        {
            TickSpecialStates();
            BeginTurn();
        }
        Clear();
        TurnPlaner.Instance.Reset();
        queueController.Clear();
    }
    private void TickSpecialStates()
    {
        //Tick all buffs/debuffs on enemies
        foreach (var item in EnemyGroup.enemies)
        {
            var debuffs = item.GetComponentsInChildren<IDisappearAfterTurn>();
            foreach (var debuff in debuffs)
            {
                debuff.Tick();
            }
        }

        //Tick all buffs//debuffs on sheep
        foreach (GameObject go in SheepGroup.Sheep)
        {
            var objs = go.GetComponents<IDisappearAfterTurn>();
            foreach (var item in objs)
                item.Tick();
        }
    }
    public void CancelSkill()
    {
        selector.StartSearching(SelectSheep, CanSheepBeSelected);
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        resourcesController.MoveFromTakenToAvailable(SelectedSkill.Cost);
        SelectedSkill = null;
        cancelButton.Hide();
    }
    public void CancelSkillSelection()
    {
        selector.StartSearching(SelectSheep, CanSheepBeSelected);
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        resourcesController.MoveFromBufferToAvailable(SelectedSkill.Cost);
        SelectedSkill = null;
        cancelButton.Hide();
    }
    private void Clear()
    {
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        resourcesController.ResetState();
        cancelButton.Hide();
        SelectedSheep = null;
        SelectedSkill = null;
    }

    private bool CanSheepBeSelected(GameObject sheep)
    {
        if (!sheep.CompareTag("Sheep"))
            return false;
        if (sheep == SelectedSheep)
            return false;
        var status = sheep.GetComponent<EntityStatus>();

        if (!status.Alive)
            return false;
        if (status.Stunned)
            return false;

        return true;
    }

    private bool CanEntityBeTarget(GameObject entity)
    {
        if (!(entity.CompareTag("Sheep") || entity.CompareTag("Enemy")))
            return false;

        var state = entity.GetComponent<EntityStatus>();
        if (!state.Targetable)
            return false;

        if (!state.Alive)
            return false;

        return true;
    }


}
