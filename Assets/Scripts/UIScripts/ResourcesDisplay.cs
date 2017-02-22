using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.LogicSystem;

public class ResourcesDisplay : MonoBehaviour
{

    public ResourceCounter ResourceCtr;
    public List<GameObject> Crystals;
    private Color active = Color.white;
    private Color inActive = new Color(0.5f, 0.5f, 0.5f, 1);
    private int currentResources;

    // Use this for initialization
    void Start()
    {
        Events.Instance.RegisterForEvent("ShowResourcesDisplay", Show);
        Events.Instance.RegisterForEvent("BattleWon", Hide);
        Events.Instance.RegisterForEvent("BattleLost", Hide);
        Events.Instance.RegisterForEvent("ChangeActive", ChangeActive);
        Events.Instance.RegisterForEvent("CleanActive", CleanActive);
        Events.Instance.RegisterForEvent("SetFilled", SetFilled);
        CleanActive(null);

        for (int i = 0; i < ResourceCtr.BasicResources; i++)
            Crystals[i].gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    void Show(object obj)
    {
        gameObject.SetActive(true);
    }

    void Hide(object obj)
    {
        gameObject.SetActive(false);
    }

    void ChangeActive(object obj)
    {
        CleanActive(null);
        int? count = obj as int?;
        if (count != null)
        {
            for (int i = currentResources - 1; i >= currentResources - count; i--)
            {
                Crystals[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    void SetFilled(object obj)
    {
        CleanActive(null);
        int? count = obj as int?;
        currentResources = (int)count;
        if (count != null)
        {
            for (int i = 0; i < Crystals.Count; i++)
            {
                Crystals[i].transform.GetChild(1).gameObject.GetComponent<Image>().color = inActive;
            }

            for (int i = 0; i < count; i++)
            {
                Crystals[i].transform.GetChild(1).gameObject.GetComponent<Image>().color = active;
            }
        }
    }

    void CleanActive(object obj)
    {
        for (int i = 0; i < Crystals.Count; i++)
        {
            Crystals[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }


}
