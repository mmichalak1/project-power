using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour {

    public void SkillButtonAction(Skill skill)
    {
        TurnManager.SelectSkill(skill);
    }
}
