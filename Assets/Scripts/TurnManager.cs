using UnityEngine;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System;

public class TurnManager : MonoBehaviour
{
    public static bool ourTurn;
    public Button turnButton;
    public static bool isSkillActive;
    public static string skillName;
    private GameObject selectedSheep;
    public const int maxResource = 10;
    public static int currentResource;
    WolfGroupManager wolfManager;

    public static bool ChangeFlag = false;
    private bool SelectingTarget = true;

    // Use this for initialization
    void Start()
    {
        ourTurn = false;
        isSkillActive = false;
        
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
        if (ourTurn)
        {
#if UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
#endif
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                checkTouch(Input.mousePosition);
            }
#endif
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
        SelectingTarget = true;

        isSkillActive = false;
        skillName = null;

        ourTurn = false;

        if (!TurnPlaner.Instance.Execute())
            return;
        wolfManager.ApplyGroupTurn();
       
        ourTurn = true;
        currentResource = 10;
        UpdateResource(0);
    }

    void checkTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && SelectingTarget)
        {
            Debug.Log(hit.transform.tag);
            if (isSkillActive)
            {
                Debug.Log(hit.collider.transform.name);
                if (hit.collider.tag == "Sheep" || hit.collider.tag == "Enemy")
                {
                    Debug.Log("Collider Hited");
                    if (skillName == "HealSkill")
                    {
                        if (currentResource - 2 >= 0)
                        {
                            TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(
                            selectedSheep, hit.collider.transform.gameObject, (actor, target) =>
                            {
                                target.GetComponent<HealthController>().Heal(5);
                            }));
                            var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");
                            foreach (GameObject X in UIElementsToDestroy)
                            {
                                Debug.Log("Destroy " + X.name);
                                Destroy(X);
                            }
                            isSkillActive = false;
                            selectedSheep = null;
                            UpdateResource(2);
                        }
                    }
                    else
                    {
                        if (currentResource - 3 >= 0)
                        {
                            TurnPlaner.Instance.AddPlan(selectedSheep.name, new Plan(
                                selectedSheep, hit.collider.transform.gameObject, (actor, target) =>
                                {
                                    target.GetComponent<HealthController>().DealDamage(55);
                                }));
                            var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

                            foreach (GameObject X in UIElementsToDestroy)
                            {
                                Debug.Log("Destroy " + X.name);
                                Destroy(X);
                            }
                            isSkillActive = false;
                            selectedSheep = null;
                            UpdateResource(3);
                        }
                    }
                }
            }
            else if (hit.transform.gameObject.tag == "Sheep")
            {
                SelectingTarget = false;
                selectedSheep = hit.transform.gameObject;
                Events.Instance.DispatchEvent(hit.transform.gameObject.name + "skill", hit.transform.gameObject);
            }
            else
            {
                Debug.Log("Raycast hit: " + hit.collider.transform.name);
            }

        }

        if (ChangeFlag)
        {
            ChangeFlag = false;
            SelectingTarget = true;
        }

    }

    public static void UpdateResource(int i)
    {
        currentResource -= i;
        Events.Instance.DispatchEvent("SetText", "Resource Left : " + currentResource);
    }
}
