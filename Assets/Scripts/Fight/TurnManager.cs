﻿using UnityEngine;
using Assets.LogicSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public static bool ourTurn = false;
    public static Skill pickedSkill;
    public SheepDataHolder[] DataHolders;
    private GameObject selectedSheep;
    public const int maxResource = 10;
    public static int currentResource;
    static WolfGroupManager wolfManager;
    public static activeState state = activeState.nothingPicked;
    public static GameObject hitedTarget = null;

    public static bool ChangeFlag = false;
    private bool SelectingTarget = true;


    public static WolfGroupManager WolfManager
    {
        get { return wolfManager; }
    }

    // Use this for initialization
    void Start()
    {
        ourTurn = false;

        currentResource = 10;
        UpdateResource(0);

        Events.Instance.RegisterForEvent("EnterFight", x =>
        {
            wolfManager = x as WolfGroupManager;

        });

        Events.Instance.RegisterForEvent("BattleWon", x =>
        {
            ourTurn = false;
            SelectingTarget = false;
            state = activeState.nothingPicked;
            FightingSceneUIScript.DisableSkillCanvases();
        });

        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            Events.Instance.RegisterForEvent(item.name, x =>
            {
                selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
            });
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
                        StartCoroutine(Wait(0.1f));
                    }
                    break;
                default:
                    break;
            }
    }

    public void ChangeTurn()
    {
        FightingSceneUIScript.DisableSkillCanvases();

        state = activeState.nothingPicked;
        //pickedSkill = null;

        ourTurn = false;

        if (!TurnPlaner.Instance.Execute())
            return;
        wolfManager.CheckForDeadAndApplyExperience();
        wolfManager.ApplyGroupTurn();


        foreach (SheepDataHolder skills in DataHolders)
            skills.SheepData.SheepSkills.UpdateCooldowns();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            go.transform.GetChild(1).GetComponent<SkillCanvasScript>().UpdateSkillsState();
            var objs = go.GetComponents<Assets.Scripts.Interfaces.IDisappearAfterTurn>();
            foreach (var item in objs)
                item.Tick();
        }

        ourTurn = true;
        currentResource = 10;
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
                var skill = pickedSkill;
                if (currentResource - skill.Cost >= 0 || TurnPlaner.Instance.ContainsPlanForSheepSkill(selectedSheep.name, skill))
                {
                    skill.OnSkillPlanned(selectedSheep, hitedTarget.transform.gameObject);
                    TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(selectedSheep, hitedTarget.transform.gameObject, skill));
                    hitedTarget = null;
                    UpdateResource(skill.Cost);
                }

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

    }

    bool GetPointerPosition()
    {
#if UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                CheckTouch(Input.GetTouch(0).position);
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            CheckTouch(Input.mousePosition);
            return true;
        }
        return false;
#endif
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

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        hitedTarget = null;
        state = activeState.skillPicked;
    }

    public enum activeState
    {
        sheepPicked,
        skillPicked,
        nothingPicked,
        waiting
    };
}