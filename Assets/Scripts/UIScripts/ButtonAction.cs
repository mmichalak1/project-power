using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour {

    public void SkillButtonAction()
    {
        TurnManager.SelectSkill(EventSystem.current.currentSelectedGameObject.GetComponent<ButtonHoldScript>().MySkill);
    }
}
