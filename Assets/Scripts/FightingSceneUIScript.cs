#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour
{

    public GameObject SkillIconsPrefab;
    public Text TextLabel;

    [SerializeField]
    private Sprite EmptySkillSprite;
    private List<GameObject> UIButtons = new List<GameObject>();
    private RectTransform containerRectTransform;
    private RectTransform prefabRectTransform;

    private List<GameObject> sheeps;


    // Use this for initialization
    void Start()
    {

        containerRectTransform = gameObject.GetComponent<RectTransform>();
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach (GameObject sheep in sheeps)
        {
            Events.Instance.RegisterForEvent(sheep.name + "skill", CreateSkillButtons);
        }

        Events.Instance.RegisterForEvent("SetText", SetText);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void CreateSkillButtons(object obj)
    {
        GameObject sheep = obj as GameObject;
        Vector3 wp = Camera.main.WorldToScreenPoint(sheep.transform.position);
        Vector2 touchPos = new Vector2(wp.x, wp.y);

        var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject X in UIElementsToDestroy)
        {
            Debug.Log("Destroy " + X.name);
            Destroy(X);
        }

        for (int i = 0; i < 4; i++)
            CreateBubble(touchPos, SkillIconsPrefab, i, sheep);
    }

    void CreateBubble(Vector2 touchPos, GameObject prefab, int ordinal, GameObject sheep)
    {
        prefabRectTransform = prefab.GetComponent<RectTransform>();
        float ratioX = containerRectTransform.rect.width / Camera.main.pixelWidth;
        float ratioY = containerRectTransform.rect.height / Camera.main.pixelHeight;
        float radius = 100 * Screen.currentResolution.width / containerRectTransform.rect.width;
        float angle = -Mathf.PI * 3 / 18;

        //create a new item, name it, and set the parent
        GameObject newItem = Instantiate(prefab) as GameObject;
        newItem.name = prefab.name;
        newItem.transform.SetParent(transform, false);

        ApplySkillData(newItem, ordinal, sheep.GetComponent<SheepDataHolder>().SheepData.SheepSkills);

        newItem.GetComponent<Button>().onClick.AddListener(() =>
        {
            TurnManager.state = TurnManager.activeState.waiting;
            TurnManager.skillName = newItem.name;
            TurnManager.ChangeFlag = true;
            TurnManager.hitedTarget = null;
        });

        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        float x = (touchPos.x + Mathf.Cos((ordinal - 1) * angle) * radius);
        float y = (touchPos.y + Mathf.Sin((ordinal - 1) * angle) * radius);

        rectTransform.position = new Vector3(x, y, 0);

        UIButtons.Add(newItem);
    }


    public void Cancel()
    {
        TurnManager.state = TurnManager.activeState.nothingPicked;
        var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject X in UIElementsToDestroy)
        {
            Debug.Log("Destroy " + X.name);
            Destroy(X);
        }
    }

    void SetText(object text)
    {
        TextLabel.text = (string)text;
    }

    void ApplySkillData(GameObject skillIcon, int number, SkillHolder holder)
    {
        if (holder.Skills[number] != null)
        {
            skillIcon.GetComponent<Image>().sprite = holder.Skills[number].Icon;
            skillIcon.name = holder.Skills[number].name;
        }
        else
        {
            skillIcon.GetComponent<Image>().sprite = EmptySkillSprite;
            skillIcon.name = "Empty Skill";
        }
    }
}
