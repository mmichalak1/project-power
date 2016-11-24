using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllSkils", menuName = "Game/Skills/AllSkills")]
public class SkillsHolder : ScriptableObject {

    public List<Skill> Skills = new List<Skill>();
}
