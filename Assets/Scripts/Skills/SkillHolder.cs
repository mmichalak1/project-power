using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/SkillHolder")]
public class SkillHolder : ScriptableObject {

    [SerializeField]
    private List<Skill> _skills;
    [HideInInspector]
    public List<Skill> Skills
    {
        get { return _skills; }
    }
}
