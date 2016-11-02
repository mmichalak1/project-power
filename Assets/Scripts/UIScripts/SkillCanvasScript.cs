using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.LogicSystem;

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
            ApplySkillData(SkillButtons[i], skills[i]);
            //SkillButtonAction(i);
        }

        Events.Instance.RegisterForEvent("EnterFight", x =>
        {
            UpdateSkillsState();
        }

        );

        gameObject.SetActive(false);
    }

    public void UpdateSkillsState()
    {
        for (int i = 0; i < SkillButtons.Length; i++)
        {
            if (skills[i] != null)
            {
                //Debug.Log(skills[i].name + "    " + skills[i].Cooldown);
                if (skills[i].Cooldown <= 0)
                {
                    //SkillButtons[i].GetComponent<Button>().enabled = true;
                    SkillButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    SkillButtons[i].transform.GetComponentInChildren<Text>().enabled = false;
                    SkillButtons[i].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    //SkillButtons[i].GetComponent<Button>().enabled = false;
                    SkillButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    SkillButtons[i].GetComponent<Image>().color = Color.grey;
                    SkillButtons[i].transform.GetComponentInChildren<Text>().enabled = true;
                    SkillButtons[i].transform.GetComponentInChildren<Text>().text = skills[i].Cooldown.ToString();
                }

                if (!skills[i].IsActive)
                {
                    //SkillButtons[i].GetComponent<Button>().enabled = false;
                    SkillButtons[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
                    SkillButtons[i].GetComponent<Image>().color = Color.grey;
                }
            }
        }
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
            skillIcon.GetComponent<ButtonHoldScript>().MySkill = Skill;
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
