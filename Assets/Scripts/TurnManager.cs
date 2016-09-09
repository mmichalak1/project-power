﻿using UnityEngine;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System;

public class TurnManager : MonoBehaviour
{
    public static bool ourTurn;
    public static string skillName;
    private GameObject selectedSheep;
    public const int maxResource = 10;
    public static int currentResource;
    WolfGroupManager wolfManager;
    public static activeState state = activeState.nothingPicked;
    public static GameObject hitedTarget = null;

    public static bool ChangeFlag = false;
    private bool SelectingTarget = true;

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
            ourTurn = true;
            SelectingTarget = true;
            var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

            foreach (GameObject X in UIElementsToDestroy)
            {
                Debug.Log("Destroy " + X.name);
                Destroy(X);
            }
        });

        foreach (var item in GameObject.FindGameObjectsWithTag("Sheep"))
        {
            Events.Instance.RegisterForEvent(item.name, x =>
            {
                selectedSheep = ((KeyValuePair<Vector2, Transform>)x).Value.gameObject;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state.ToString());
        GetPointerPosition();
        if (ourTurn && hitedTarget != null)
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
        var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject X in UIElementsToDestroy)
        {
            Debug.Log("Destroy " + X.name);
            Destroy(X);
        }

        state = activeState.nothingPicked;
        skillName = null;

        ourTurn = false;

        if (!TurnPlaner.Instance.Execute())
            return;
        wolfManager.ApplyGroupTurn();

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
                if (skillName == "HealSkill")
                {
                    if (currentResource - 2 >= 0)
                    {
                        TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(
                        selectedSheep, hitedTarget.transform.gameObject, (actor, target) =>
                        {
                            target.GetComponent<HealthController>().Heal(5);
                        }));
                        hitedTarget = null;
                        UpdateResource(2);
                    }
                }
                else
                {
                    if (currentResource - 3 >= 0)
                    {
                        TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(
                            selectedSheep, hitedTarget.transform.gameObject, (actor, target) =>
                            {
                                target.GetComponent<HealthController>().DealDamage(55);
                            }));
                        hitedTarget = null;
                        UpdateResource(3);
                    }
                }

                var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");
                foreach (GameObject X in UIElementsToDestroy)
                {
                    Debug.Log("Destroy " + X.name);
                    Destroy(X);
                }

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
        }
    }

    void GetPointerPosition()
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
        }
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
