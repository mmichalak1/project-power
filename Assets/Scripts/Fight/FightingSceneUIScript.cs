#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Linq;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour
{
    private static List<GameObject> sheeps;

    // Use this for initialization
    void Start()
    {
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach (GameObject sheep in sheeps)
        {
            Events.Instance.RegisterForEvent(sheep.name + "skill", EnableSkillButtons);
        }

        gameObject.SetActive(false);
    }

    void EnableSkillButtons(object obj)
    {
        GameObject sheep = obj as GameObject;
        DisableSkillCanvases();
        sheep.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Cancel()
    {
        TurnManager.state = TurnManager.activeState.nothingPicked;
        Events.Instance.DispatchEvent("CleanActive", null);
        DisableSkillCanvases();
    }

    public static void DisableSkillCanvases()
    {
        for (int i = 0; i < sheeps.Count; i++)
        {
            if (sheeps[i] != null)
            {
                sheeps[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
