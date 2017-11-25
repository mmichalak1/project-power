using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SkillData
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public static void CreateFromSavedData(ref Skill skill, SkillData skillData)
    {
        skill.IsActive = skillData.IsActive;
    }

    public static void CreateFromRuntime(Skill skill, ref SkillData data)
    {
        data.Name = skill.Name;
        data.IsActive = skill.IsActive;
    }
}
