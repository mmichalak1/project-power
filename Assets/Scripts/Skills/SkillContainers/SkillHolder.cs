#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/SkillHolder")]
public class SkillHolder : ScriptableObject
{

    public void UpdateCooldowns()
    {
        foreach (var skill in _skills)
        {
            if (skill != null)
                skill.UpdateCooldown();
        }
    }

    public void ResetCooldowns()
    {
        foreach (var skill in _skills)
            if (skill != null)
                skill.ResetCooldown();
    }

    [SerializeField]
    private List<Skill> _skills;
    [HideInInspector]
    public List<Skill> EquipedSkills
    {
        get { return _skills; }
    }

    [SerializeField]
    private SkillTree skillTree;
    public SkillTree SkillTree { get { return skillTree; }}
}
