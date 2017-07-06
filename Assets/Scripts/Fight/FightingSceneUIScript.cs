#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Linq;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour
{
    public Text TextLabel;

    private static List<GameObject> sheeps;

    // Use this for initialization
    void Start()
    {
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach (GameObject sheep in sheeps)
        {
            var holder = sheep.GetComponent<EntityDataHolder>();
            Events.Instance.RegisterForEvent(holder.SheepData.name + "skill", EnableSkillButtons);
        }

        Events.Instance.RegisterForEvent("SetText", SetText);
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

    void SetText(object text)
    {
        TextLabel.text = (string)text;
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
