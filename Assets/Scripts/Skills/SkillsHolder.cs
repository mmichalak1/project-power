using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllSkills", menuName = "Game/Skills/AllSkills")]
public class SkillsHolder : ScriptableObject {

    public List<Skill> Skills = new List<Skill>();
}
