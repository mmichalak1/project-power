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

    public void loadSkillsData(SkillHolder skillHolder)
    {
        int activeSkillCount = 0;
        for(int i = 0; i < SkillButtons.Count; i++)
        {
            if (skillHolder.Skills[i].IsActive)
            {
                SkillButtons[activeSkillCount].gameObject.SetActive(true);
                SkillButtons[activeSkillCount].Skill = skillHolder.Skills[i];
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
