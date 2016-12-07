using UnityEngine;
using Assets.LogicSystem;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(TurnPlayer))]
public class TurnManager : MonoBehaviour
{
    #region Statics
    private static TurnManager Instance;
    private static WolfGroupManager _wolfManager;
    public static activeState state = activeState.nothingPicked;
    public static GameObject hitedTarget = null;
    public static int currentResource;
    public static bool ourTurn = false;
    public static Skill pickedSkill;
    //public static bool ChangeFlag = false;
    public static bool BattleWon = false;

    public static WolfGroupManager WolfManager
    {
        get { return _wolfManager; }
    }


    public static void UpdateResource(int i)
    {
        currentResource -= i;
        Events.Instance.DispatchEvent("SetText", "Resource Left : " + currentResource);
    }

    public static void SelectSkill(Skill selectedSkill)
    {

        if (currentResource >= selectedSkill.Cost || TurnPlaner.Instance.ContainsPlanForSheepSkill(hitedTarget.name, selectedSkill))
        {
            pickedSkill = selectedSkill;
            Events.Instance.DispatchEvent("SetText", "Resource Left : " + currentResource + " - " + selectedSkill.Cost);
            state = activeState.waiting;
        }
        else
        {
            Instance.OnNotEnoughResources();
            FightingSceneUIScript.DisableSkillCanvases();
            state = activeState.reseting;
        }
        //ChangeFlag = true;
        hitedTarget = null;

    }

    #endregion

    [SerializeField]
    private ResourceCounter DefaultResourceCounter;

    public GameObject ChangeTurnButton;
    public GameObject ConfirmEndTurn;
    public GameObject ExplorationUI;
    public GameObject BattleUI;
    private GameObject selectedSheep;


    public FadeInAndOut Fader;
    public EntityDataHolder[] DataHolders;
    public ActionBubble[] actionBubbles;

    private bool SelectingTarget = true;
    private TurnPlayer turnPlayer;
    private int WoolForFight = 0;


    #region Events
    private void OnEnterFight(object x)
    {
        _wolfManager = x as WolfGroupManager;
        WoolForFight = _wolfManager.WoolForFight;
        ChangeTurnButton.SetActive(false);
    }
    private void OnBattleWon(object x)
    {
        FightingSceneUIScript.DisableSkillCanvases();
    }
    private void OnShowChangTurnButton(object x)
    {
        ChangeTurnButton.SetActive(true);
    }
    private void OnSheepSelected(object x)
    {
        selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
    }
    #endregion

   
    public void ChangeTurn(bool forced)
    {
        #region ClearScreen & reset state
        if (!forced && currentResource == DefaultResourceCounter.Resources)
        {
            ConfirmEndTurn.SetActive(true);
            return;
        }

        foreach (ActionBubble item in actionBubbles)
        {
            item.TurnOff();
        }

        FightingSceneUIScript.DisableSkillCanvases();
        state = activeState.nothingPicked;
        ourTurn = false;

        #endregion

        //Let wolves plan their turn
        _wolfManager.ApplyGroupTurn();

        //Order TurnPlayer to start playing every move
        turnPlayer.PlayTurn(PostTurnActions);

    }

    #region StateBehaviours
    void NothingPickedActions()
    {
        if (hitedTarget.tag == "Sheep")
        {
            state = activeState.sheepPicked;
            selectedSheep = hitedTarget;
            Events.Instance.DispatchEvent(hitedTarget.transform.gameObject.name + "skill", hitedTarget.transform.gameObject);
        }
    }

    void SkillPickedActions()
    {
        if (hitedTarget != null)
            if (hitedTarget.tag == "Sheep" || hitedTarget.tag == "Enemy")
            {
                Plan plan = new Plan(selectedSheep, hitedTarget.transform.gameObject, pickedSkill);

                if (!TurnPlaner.Instance.ContainsPlanForSheepSkill(selectedSheep.name, pickedSkill))
                {
                    UpdateResource(pickedSkill.Cost);
                    EntityDataHolder sheepDataHolder = (EntityDataHolder)plan.Actor.GetComponent(typeof(EntityDataHolder));
                    var bubble = actionBubbles[Array.IndexOf(DataHolders, sheepDataHolder)];
                    bubble.TurnOn();
                }
                pickedSkill.OnSkillPlanned(selectedSheep, hitedTarget.transform.gameObject);
                TurnPlaner.Instance.AddPlan(selectedSheep.name, plan);
                hitedTarget = null;

                FightingSceneUIScript.DisableSkillCanvases();
                state = activeState.nothingPicked;
            }
    }

    void SheepPickedActions()
    {
        if (hitedTarget.tag == "Sheep" && hitedTarget != selectedSheep)
        {
            state = activeState.sheepPicked;
            selectedSheep = hitedTarget.transform.gameObject;
            Events.Instance.DispatchEvent(hitedTarget.transform.gameObject.name + "skill", hitedTarget.transform.gameObject);
            return;
        }
        else
        {
            FightingSceneUIScript.DisableSkillCanvases();
            selectedSheep = null;
        }

    }

    bool GetPointerPosition()
    {
#if UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                CheckTouch(Input.GetTouch(0).position);
                return true;
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            CheckTouch(Input.mousePosition);
            return true;
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


        if (BattleWon)
        {
            BattleUI.SetActive(false);
            ExplorationUI.SetActive(true);
            Events.Instance.DispatchEvent("AfterBattleScreen", WoolForFight);
            Events.Instance.DispatchEvent("BattleWon", null);
            ResetToDefault();
        }
        else
        {
            TickSpecialStates();
            ourTurn = true;
            currentResource = DefaultResourceCounter.Resources;
            UpdateResource(0);
        }
        //Reset to starting state
        TurnPlaner.Instance.Reset();
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
        currentResource = DefaultResourceCounter.Resources;
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

        currentResource = DefaultResourceCounter.Resources;
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


    public enum activeState
    {
        sheepPicked,
        skillPicked,
        nothingPicked,
        reseting,
        waiting
    };
}
