using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour {

    public void SkillButtonAction()
    {
        TurnManager.state = TurnManager.activeState.waiting;
        TurnManager.pickedSkill = EventSystem.current.currentSelectedGameObject.GetComponent<ButtonHoldScript>().MySkill;
        TurnManager.ChangeFlag = true;
        TurnManager.hitedTarget = null;
    }
}
