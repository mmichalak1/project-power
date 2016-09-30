using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillCanvasScript : MonoBehaviour
{

    private List<Skill> skills;
    public GameObject[] SkillButtons;

    [SerializeField]
    private Sprite EmptySkillSprite;

    // Use this for initialization
    void Start()
    {
        skills = gameObject.GetComponentInParent<SheepDataHolder>().SheepData.SheepSkills.Skills;

        for (int i = 0; i < SkillButtons.Length; i++)
        {
            ApplySkillData(SkillButtons[i],skills[i]);
            SkillButtonAction(i);
        }

        gameObject.SetActive(false);
    }

    void SkillButtonAction(int ordinal)
    {
        Debug.Log(skills[ordinal].Cooldown);
        if (skills[ordinal].Cooldown <= 0)
            SkillButtons[ordinal].GetComponent<Button>().onClick.AddListener(() =>
            {
                TurnManager.state = TurnManager.activeState.waiting;
                TurnManager.skillName = skills[ordinal].name;
                TurnManager.ChangeFlag = true;
                TurnManager.hitedTarget = null;
            });
        else
            SkillButtons[ordinal].GetComponent<Button>().enabled = false;
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
