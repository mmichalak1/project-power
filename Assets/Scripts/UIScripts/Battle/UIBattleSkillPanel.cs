using UnityEngine;
using System.Collections.Generic;

public class UIBattleSkillPanel : MonoBehaviour
{
    public List<UIBattleSkillButton> SkillButtons;

    void loadSkillsData(SkillHolder skillHolder)
    {
        for(int i = 0; i < SkillButtons.Count; i++)
        {
            if (skillHolder.Skills[i].IsActive)
                SkillButtons[i].Skill = skillHolder.Skills[i];
            else
                SkillButtons[i].gameObject.SetActive(false);
        }
    }
}
