using UnityEngine;
using Assets.LogicSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    private static TurnManager Instance;
    static WolfGroupManager wolfManager;
    public static activeState state = activeState.nothingPicked;
    public static GameObject hitedTarget = null;
    public static int currentResource;
    public static bool ourTurn = false;
    public static Skill pickedSkill;
    public static bool ChangeFlag = false;
    public GameObject ChangeTurnButton;

    private ActionBubble actionBubbleCleric;
    private ActionBubble actionBubbleMage;
    private ActionBubble actionBubbleRouge;
    private ActionBubble actionBubbleWarrior;

    [SerializeField]
    private ResourceCounter DefaultResourceCounter; 

    public FadeInAndOut Fader;
    public EntityDataHolder[] DataHolders;

    private GameObject selectedSheep;
    private bool SelectingTarget = true;


    public static WolfGroupManager WolfManager
    {
        get { return wolfManager; }
    }

    // Use this for initialization
    void Start()
    {
        ourTurn = false;
        Instance = this;

        currentResource = DefaultResourceCounter.Resources;
        UpdateResource(0);

        Events.Instance.RegisterForEvent("EnterFight", x =>
        {
            wolfManager = x as WolfGroupManager;
            ChangeTurnButton.SetActive(false);
        });

        Events.Instance.RegisterForEvent("BattleWon", x =>
        {
            ourTurn = false;
            SelectingTarget = false;
            state = activeState.nothingPicked;
            FightingSceneUIScript.DisableSkillCanvases();
        });

        Events.Instance.RegisterForEvent("ShowChangeTurnButton", x => { ChangeTurnButton.SetActive(true); });

        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            Events.Instance.RegisterForEvent(item.name, x =>
            {
                selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
            });
        }

        GameObject player = GameObject.Find("Player");
        List<RectTransform> listyOfAllImages = player.transform.GetComponentsInChildren<RectTransform>(true).ToList<RectTransform>();

        actionBubbleCleric = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleCleric").GetComponent(typeof(ActionBubble));
        actionBubbleMage = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleMage").GetComponent(typeof(ActionBubble));
        actionBubbleRouge = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleRouge").GetComponent(typeof(ActionBubble));
        actionBubbleWarrior = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleWarrior").GetComponent(typeof(ActionBubble));
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

    public void ChangeTurn()
    {
        actionBubbleCleric.TurnOff();
        actionBubbleMage.TurnOff();
        actionBubbleRouge.TurnOff();
        actionBubbleWarrior.TurnOff();

        FightingSceneUIScript.DisableSkillCanvases();

        state = activeState.nothingPicked;
        //pickedSkill = null;

        ourTurn = false;

        if (!TurnPlaner.Instance.Execute())
        {
            return;
        }
            
        wolfManager.ApplyGroupTurn();


        foreach (EntityDataHolder skills in DataHolders)
            skills.SheepData.SheepSkills.UpdateCooldowns();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            go.transform.GetChild(0).GetComponent<SkillCanvasScript>().UpdateSkillsState();
            var objs = go.GetComponents<Assets.Scripts.Interfaces.IDisappearAfterTurn>();
            foreach (var item in objs)
                item.Tick();
        }

        ourTurn = true;
        currentResource = DefaultResourceCounter.Resources;
        UpdateResource(0);
    }

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
                    switch (sheepDataHolder.SheepData.SheepClass)
                    {
                        case EntityData.Class.Cleric:
                            actionBubbleCleric.TurnOn();
                            actionBubbleCleric.SetImage(plan.Skill.Icon);
                            break;
                        case EntityData.Class.Mage:
                            actionBubbleMage.TurnOn();
                            actionBubbleMage.SetImage(plan.Skill.Icon);
                            break;
                        case EntityData.Class.Rouge:
                            actionBubbleRouge.TurnOn();
                            actionBubbleRouge.SetImage(plan.Skill.Icon);
                            break;
                        case EntityData.Class.Warrior:
                            actionBubbleWarrior.TurnOn();
                            actionBubbleWarrior.SetImage(plan.Skill.Icon);
                            break;
                        default:
                            break;
                    }
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

    void CheckTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitedTarget = hit.collider.gameObject;
        }
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
        ChangeFlag = true;
        hitedTarget = null;

    }

    private void OnNotEnoughResources()
    {
        Fader.Play();
    }

    public IEnumerator Wait(float time, activeState targetState)
    {
        yield return new WaitForSeconds(time);
        hitedTarget = null;
        state = targetState;
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
