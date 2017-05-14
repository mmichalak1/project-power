using UnityEngine;
using Assets.LogicSystem;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(TurnPlayer))]
public class TurnManager : MonoBehaviour
{
    #region Statics
    private static TurnManager Instance;
    private static WolfGroupManager _wolfManager;
    public static activeState state = activeState.nothingPicked;
    public static GameObject hitedTarget = null;
    public static int CurrentResource { get; set; }
    public static bool ourTurn = false;
    public static Skill pickedSkill;
    public static bool BattleWon { get; set; }

    public static WolfGroupManager WolfManager
    {
        get { return _wolfManager; }
    }


    public static void UpdateResource(int i)
    {
        CurrentResource -= i;
        if (CurrentResource > Instance.DefaultResourceCounter.Resources)
            CurrentResource = Instance.DefaultResourceCounter.Resources;
        Events.Instance.DispatchEvent("SetFilled", CurrentResource);
    }

    public static void SelectSkill(Skill selectedSkill)
    {
        //Check for no resources
        if (CurrentResource < selectedSkill.Cost)
        {
            Instance.OnNotEnoughResources();
            FightingSceneUIScript.DisableSkillCanvases();
            state = activeState.reseting;
            return;
        }

        //if resources are ok change picked skill and notify resuources display
        pickedSkill = selectedSkill;
        if (!TurnPlaner.Instance.ContainsPlanWithSkill(selectedSkill))
            Events.Instance.DispatchEvent("ChangeActive", selectedSkill.Cost);
        state = activeState.waiting;

        hitedTarget = null;

    }

    #endregion

    [SerializeField]
    private ResourceCounter DefaultResourceCounter;

    public UIBattle UIBattle;

    public GameObject ConfirmEndTurn;
    public GameObject ExplorationUI;
    public GameObject BattleUI;
    private GameObject _selectedSheep;
    private GameObject selectedSheep
    {
        get { return _selectedSheep; }
        set
        {
            if (_selectedSheep != null)
                _selectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(false);
            _selectedSheep = value;
        }
    }

    public FadeInAndOut Fader;
    public EntityDataHolder[] DataHolders;
    public ActionBubble[] actionBubbles;
    public TurnQueueController queueController;

    private TurnPlayer turnPlayer;
    private int WoolForFight = 0;

    #region Events
    private void OnEnterFight(object x)
    {
        _wolfManager = x as WolfGroupManager;
        WoolForFight = _wolfManager.WoolForFight;
        queueController.gameObject.SetActive(false);
    }
    private void OnBattleWon(object x)
    {
        FightingSceneUIScript.DisableSkillCanvases();
    }
    private void OnShowChangTurnButton(object x)
    {
        queueController.gameObject.SetActive(true);
    }
    private void OnSheepSelected(object x)
    {
        selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
    }
    #endregion


    public void ChangeTurn(bool forced)
    {
        #region ClearScreen & reset state
        if (ourTurn)
        {
          //  ChangeTurnButton.GetComponent<Button>().interactable = false;
            //forced is varaible which is set to true when player knows he didnt spent all resources and still wants to end turn
            if (!forced && CurrentResource == DefaultResourceCounter.Resources)
            {
                ConfirmEndTurn.SetActive(true);
                return;
            }

            foreach (ActionBubble item in actionBubbles)
            {
                item.TurnOff();
            }

            if (selectedSheep != null)
            {
                selectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(false);
                selectedSheep = null;
            }

            FightingSceneUIScript.DisableSkillCanvases();
            state = activeState.nothingPicked;
            ourTurn = false;

            #endregion


            //Order TurnPlayer to start playing every move and pass function for wolves thinking
            turnPlayer.PlayTurn(PostTurnActions, () =>
            {
                if (WolfManager != null)
                    WolfManager.ApplyGroupTurn();
            }
            );
        }

    }

    #region StateBehaviours
    void NothingPickedActions()
    {
        if (hitedTarget.tag == "Sheep")
        {
            state = activeState.sheepPicked;
            selectedSheep = hitedTarget;
            selectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(true);
            UIBattle.SkillPanel.LoadSkillsData(selectedSheep.GetComponent<EntityDataHolder>().SheepData.SheepSkills);
        }
    }

    void SkillPickedActions()
    {
        if (hitedTarget != null)
        {
            if (hitedTarget.tag == "Sheep" || hitedTarget.tag == "Enemy")
            {
                if (pickedSkill.IsTargetValid(selectedSheep, hitedTarget))
                {
                    Plan plan = new Plan(selectedSheep, hitedTarget.transform.gameObject, pickedSkill);

                    if (!TurnPlaner.Instance.ContainsPlan(plan))
                    {
                        EntityDataHolder sheepDataHolder = plan.Actor.GetComponent<EntityDataHolder>();
                        var bubble = actionBubbles[Array.IndexOf(DataHolders, sheepDataHolder)];
                        bubble.TurnOn();
                    }
                    else
                    {
                        queueController.CancelPlan(plan);
                    }
                    pickedSkill.OnSkillPlanned(selectedSheep, hitedTarget.transform.gameObject);
                    TurnPlaner.Instance.AddPlan(plan);
                    queueController.AddPlan(plan);
                }
                else
                {
                    Debug.Log("Invalid Target");
                    Events.Instance.DispatchEvent("CleanActive", null);
                }
                hitedTarget = null;

                selectedSheep = null;
                state = activeState.nothingPicked;
            }
        }
    }

    void SheepPickedActions()
    {
        if (hitedTarget.tag == "Sheep" && hitedTarget != selectedSheep)
        {
            state = activeState.sheepPicked;
            selectedSheep = hitedTarget.transform.gameObject;
            selectedSheep.transform.FindChild("SelectRing").gameObject.SetActive(true);
            UIBattle.SkillPanel.LoadSkillsData(selectedSheep.GetComponent<EntityDataHolder>().SheepData.SheepSkills);
            return;
        }
        else
        {
            FightingSceneUIScript.DisableSkillCanvases();
            selectedSheep = null;
            state = activeState.nothingPicked;
        }

    }

    bool GetPointerPosition()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonUp(0))
        {
            CheckTouch(Input.mousePosition);
            return true;
        }

#elif UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                CheckTouch(Input.GetTouch(0).position);
                return true;
            }
        }
#endif
        return false;
    }
    #endregion


    private void CheckTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitedTarget = hit.collider.gameObject;
        }
    }

    private void OnNotEnoughResources()
    {
        selectedSheep = null;
        Fader.Play();
    }

    private IEnumerator Wait(float time, activeState targetState)
    {
        yield return new WaitForSeconds(time);
        hitedTarget = null;
        state = targetState;
    }

    private void PostTurnActions()
    {
        //Tick all cooldowns for sheep
        foreach (EntityDataHolder skills in DataHolders)
            skills.SheepData.SheepSkills.UpdateCooldowns();

        //Debug.Log("Checking state");
        if (BattleWon)
        {
            var effects = FindObjectsOfType<SC_SpellDuration>();
            foreach (var effect in effects)
            {
                effect.enabled = true;
            }
            UIBattle.gameObject.SetActive(false);
            ExplorationUI.SetActive(true);
            Events.Instance.DispatchEvent("AfterBattleScreen", WoolForFight);
            Events.Instance.DispatchEvent("BattleWon", null);
            ResetToDefault();
        }
        else
        {
            TickSpecialStates();
            ourTurn = true;
            CurrentResource = DefaultResourceCounter.Resources;
            UpdateResource(0);
        }
        //Reset to starting state
   //     ChangeTurnButton.GetComponent<Button>().interactable = true;
        TurnPlaner.Instance.Reset();
        queueController.Clear();
    }
    private void TickSpecialStates()
    {
        //Tick all buffs/debuffs on enemies
        foreach (var item in WolfManager.enemies)
        {
            var debuffs = item.GetComponentsInChildren<Assets.Scripts.Interfaces.IDisappearAfterTurn>();
            foreach (var debuff in debuffs)
            {
                debuff.Tick();
            }
        }

        //Tick all buffs//debuffs on sheep
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            go.transform.GetChild(0).GetComponent<SkillCanvasScript>().UpdateSkillsState(null);
            var objs = go.GetComponents<Assets.Scripts.Interfaces.IDisappearAfterTurn>();
            foreach (var item in objs)
                item.Tick();
        }
    }
    private void ResetToDefault()
    {
        ourTurn = false;
        CurrentResource = DefaultResourceCounter.Resources;
        UpdateResource(0);
        state = activeState.nothingPicked;
        hitedTarget = null;
        BattleWon = false;
    }
    void Start()
    {
        ourTurn = false;
        Instance = this;
        turnPlayer = gameObject.GetComponent<TurnPlayer>();

        CurrentResource = DefaultResourceCounter.Resources;
        UpdateResource(0);

        Events.Instance.RegisterForEvent("EnterFight", OnEnterFight);

        Events.Instance.RegisterForEvent("BattleWon", OnBattleWon);

        Events.Instance.RegisterForEvent("ShowChangeTurnButton", OnShowChangTurnButton);

        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            Events.Instance.RegisterForEvent(item.name, OnSheepSelected);
        }
    }

    void Update()
    {
        if (ourTurn && GetPointerPosition() && hitedTarget != null)
            switch (state)
            {
                case activeState.sheepPicked:
                    {
                        SheepPickedActions();
                    }
                    break;
                case activeState.skillPicked:
                    {
                        SkillPickedActions();
                    }
                    break;
                case activeState.nothingPicked:
                    {
                        NothingPickedActions();
                    }
                    break;
                case activeState.waiting:
                    {
                        StartCoroutine(Wait(0.1f, activeState.skillPicked));
                    }
                    break;
                case activeState.reseting:
                    {
                        StartCoroutine(Wait(0.1f, activeState.nothingPicked));
                    }
                    break;
                default:
                    break;
            }
    }

    public void CancelButton()
    {
        selectedSheep = null;
    }

    public enum activeState
    {
        sheepPicked,
        skillPicked,
        nothingPicked,
        reseting,
        waiting
    };
}
