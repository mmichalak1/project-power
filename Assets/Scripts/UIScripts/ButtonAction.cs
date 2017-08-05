using UnityEngine;
using Assets.Scripts.Interfaces;

public class ButtonAction : MonoBehaviour {

    public void SkillButtonAction(Skill skill)
    {
        SystemAccessor.GetSystem<ITurnManager>().SelectSkill(skill);
    }
}
