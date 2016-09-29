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

    private List<GameObject> sheeps;
    private List<GameObject> enemies;


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

    void CreateSkillButtons(object obj)
    {
        GameObject sheep = obj as GameObject;
        Vector3 wp = Camera.main.WorldToScreenPoint(sheep.transform.position);
        Vector2 touchPos = new Vector2(wp.x, wp.y);

        var UIElementsToDestroy = GameObject.FindGameObjectsWithTag("Bubble");

        foreach (GameObject X in UIElementsToDestroy)
        {
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
        var skill = sheep.GetComponent<SheepDataHolder>().SheepData.SheepSkills.Skills[ordinal];
        ApplySkillData(newItem, skill);

        if (skill != null)
        {
            if (skill.Cooldown <= 0)
                newItem.GetComponent<Button>().onClick.AddListener(() =>
                {
                    TurnManager.state = TurnManager.activeState.waiting;
                    TurnManager.skillName = newItem.name;
                    TurnManager.ChangeFlag = true;
                    TurnManager.hitedTarget = null;
                });
            else
                newItem.GetComponent<Button>().enabled = false;
        }

        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        float x = (touchPos.x + Mathf.Cos((ordinal - 1) * angle) * radius);
        float y = (touchPos.y + Mathf.Sin((ordinal - 1) * angle) * radius);

        rectTransform.position = new Vector3(x, y, 0);
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

    void ApplySkillData(GameObject skillIcon, Skill Skill)
    {
        if (Skill != null)
        {
            if (Skill.Cooldown > 0)
            {
                skillIcon.GetComponent<Image>().color = Color.gray;
                skillIcon.GetComponentInChildren<Text>().enabled = true;
                skillIcon.GetComponentInChildren<Text>().text = Skill.Cooldown + "";
            }
            skillIcon.GetComponent<Image>().sprite = Skill.Icon;
            skillIcon.name = Skill.name;
        }
        else
        {
            skillIcon.GetComponent<Image>().sprite = EmptySkillSprite;
            skillIcon.name = "Empty Skill";
        }
    }
}
