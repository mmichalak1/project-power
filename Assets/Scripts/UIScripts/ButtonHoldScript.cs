using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonHoldScript : MonoBehaviour
{

    private bool isShown = false;
    private bool isBeingClicked = false;

    public Skill MySkill { get; set; }
    public SkillDescription SkillDescription;

    public float RequestedTouchTime = 2f;
    private float timer = 0.0f;

    public void OnPointerDown(BaseEventData data)
    {
        isBeingClicked = true;
    }

    public void OnPointerUp(BaseEventData data)
    {
        isBeingClicked = false;
        FightingSceneUIScript.DisableSkillCanvases();
        timer = 0.0f;

        if (isShown)
        {
            //What happens when button was held and now is being released
            isShown = false;
        }
        else
        {
            //OnButtonClick
            TurnManager.SelectSkill(MySkill);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingClicked)
        {
            timer += Time.deltaTime;
            if (timer > RequestedTouchTime)
            {
                if (!isShown)
                {
                    TurnManager.state = TurnManager.activeState.nothingPicked;
                    isShown = true;
                    SkillDescription.gameObject.SetActive(true);
                    SkillDescription.LoadSkillData(MySkill);
                    FightingSceneUIScript.DisableSkillCanvases();
                    Debug.Log("Hey, I was held for more than " + RequestedTouchTime);
                }
            }
        }
    }

}
