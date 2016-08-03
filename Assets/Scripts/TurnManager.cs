using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.LogicSystem;

public class TurnManager : MonoBehaviour
{

    GameObject[] enemies;
    public static bool ourTurn;
    public Button turnButton;
    public static bool isSkillActive;
    public static string skillName;
    public const int maxResource = 10;
    public static int currentResource;
    RuntimePlatform platform = Application.platform;

    // Use this for initialization
    void Start()
    {
        ourTurn = false;
        isSkillActive = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentResource = 10;
        UpdateResource(0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Resolution : " + Screen.currentResolution);
        if (ourTurn)
        {
            if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        checkTouch(Input.GetTouch(0).position);
                    }
                }
            }
            else if (platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    checkTouch(Input.mousePosition);
                }
            }
        }
    }

    public void ChangeTurn()
    {
        ourTurn = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<AttackController>().PerformAction();
        }
        ourTurn = true;
        currentResource = 10;
        UpdateResource(0);
    }

    void checkTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (isSkillActive)
            {
                if (hit.transform.gameObject.tag == "Sheep" || hit.transform.gameObject.tag == "Enemy")
                {
                    if (skillName == "HealSkill")
                    {
                        if (currentResource - 2 >= 0)
                        {
                            hit.transform.gameObject.GetComponent<HealthController>().Heal(5);
                            UpdateResource(2);
                        }
                    }
                    else
                    {
                        if (currentResource - 3 >= 0)
                        {
                            hit.transform.gameObject.GetComponent<HealthController>().DealDamage(10);
                            UpdateResource(3);
                        }
                    }
                }
            }
            else if (hit.transform.gameObject.tag == "Sheep")
            {
                Events.Instance.DispatchEvent(hit.transform.gameObject.name, hit.transform.gameObject);
            }
            
        }
    }

    public static void UpdateResource(int i)
    {
        currentResource -= i;
        Events.Instance.DispatchEvent("SetText", "Resource Left : " + currentResource);
    }
}
