#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Linq;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour
{

    public GameObject SkillIconsPrefab;
    public GameObject HealthBarPrefab;
    public Text TextLabel;

    [SerializeField]
    private Sprite EmptySkillSprite;
    private List<GameObject> UIButtons = new List<GameObject>();
    private RectTransform containerRectTransform;
    private RectTransform prefabRectTransform;

    private static List<GameObject> sheeps;
    private List<GameObject> enemies;


    // Use this for initialization
    void Start()
    {

        containerRectTransform = gameObject.GetComponent<RectTransform>();
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach (GameObject sheep in sheeps)
        {
            Events.Instance.RegisterForEvent(sheep.name + "skill", EnableSkillButtons);
        }

        Events.Instance.RegisterForEvent("SetText", SetText);
        gameObject.SetActive(false);
    }

    void EnableSkillButtons(object obj)
    {
        GameObject sheep = obj as GameObject;
        DisableSkillCanvases();
        sheep.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Cancel()
    {
        TurnManager.state = TurnManager.activeState.nothingPicked;
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
                sheeps[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
