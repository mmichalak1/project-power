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
        for(int i = 0; i < SkillButtons.Count; i++)
        {
            if (skillHolder.Skills[i].IsActive)
                SkillButtons[i].Skill = skillHolder.Skills[i];
            else
                SkillButtons[i].gameObject.SetActive(false);
        }
    }

    internal void Activate(bool isDisplayed)
    {
        animator.SetBool("isDisplayed", isDisplayed);
    }
}
