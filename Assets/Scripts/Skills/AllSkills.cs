using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AllSkills", menuName = "Game/Skills/AllSkills")]
public class AllSkills : ScriptableObject {

    public List<Skill> Skills = new List<Skill>();
}
