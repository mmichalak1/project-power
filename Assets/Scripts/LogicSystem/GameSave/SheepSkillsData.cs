using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class SheepSkillsData
{
    public static SheepSkillsData CreateFromRuntime(SkillHolder skillHolder, ref SheepSkillsData data)
    {
        foreach (var skill in skillHolder.SkillTree.AllSkills)
        {
            var skillData = new SkillData();
            SkillData.CreateFromRuntime(skill, ref skillData);
            data.SkillsData.Add(skillData);
        }

        for (int i = 1; i < skillHolder.EquipedSkills.Count; i++)
        {
            data.EquippedSkills.Add(skillHolder.EquipedSkills[0].Name);
        }

        return data;
    }

    public static void CreateFromGameSave(ref SkillHolder skills, SheepSkillsData data)
    {
        Skill skill = null;
        foreach (var skillData in data.SkillsData)
        {
            skill = skills.SkillTree.AllSkills.First(x => x.Name == skillData.Name);
            SkillData.CreateFromSavedData(ref skill, skillData);
            skill = null;
        }
        string skillName;
        for (int i = 0; i < skills.EquipedSkills.Count; i++)
        {
            skillName = data.EquippedSkills[i];
            skill = skills.SkillTree.AllSkills.First(x => x.Name == skillName);
            skills.EquipedSkills[i + 1] = skill;
            skillName = "";

        }
    }

    public List<SkillData> SkillsData { get; set; } = new List<SkillData>();
    public List<string> EquippedSkills { get; set; } = new List<string>();
}