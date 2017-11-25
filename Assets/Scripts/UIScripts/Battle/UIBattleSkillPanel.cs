using UnityEngine;
using System.Collections.Generic;
using System;

public class UIBattleSkillPanel : MonoBehaviour
{
    public List<UIBattleSkillButton> SkillButtons;
    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void LoadSkillsData(SkillHolder skillHolder)
    {
        int activeSkillCount = 0;
        for(int i = 0; i < SkillButtons.Count; i++)
        {
            if (skillHolder.EquipedSkills[i].IsActive)
            {
                SkillButtons[activeSkillCount].gameObject.SetActive(true);
                SkillButtons[activeSkillCount].Skill = skillHolder.EquipedSkills[i];
                activeSkillCount++;
            }
            else
                SkillButtons[SkillButtons.Count - 1 - (i - activeSkillCount)].gameObject.SetActive(false);
        }
    }

    internal void Activate(bool isDisplayed)
    {
        animator.SetBool("isDisplayed", isDisplayed);
    }
}
