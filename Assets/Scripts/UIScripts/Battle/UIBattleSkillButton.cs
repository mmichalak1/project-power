using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Assets.LogicSystem;

public class UIBattleSkillButton : MonoBehaviour
{
    public Image skillIcon;

    private Animator animator;
    private Skill skill;
    private Vector2? touchStart = -Vector2.one;
    private int MINDISTANCE = 10;
    private bool isDisplayed = false;

    public Skill Skill
    {
        set
        {
            skill = value;
            skillIcon.sprite = skill.Icon;
        }
    }

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Events.Instance.RegisterForEvent("HideBattleSkillPanel", Hide);
    }

    public void Hide(object obj)
    {
        if (isDisplayed)
            isDisplayed = false;
        animator.SetBool("isDisplayed", isDisplayed);
    }

    public void OnTouchStart()
    {
        touchStart = Input.mousePosition;
    }

    public void OnTouchStop()
    {
        if (touchStart != null)
        {
            var touch = (Vector2)Input.mousePosition;
            if (touch.x > touchStart.Value.x && Mathf.Abs(touch.x - touchStart.Value.x) > MINDISTANCE)
            {
                Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
                isDisplayed = true;
            }
            else
            {
                isDisplayed = false;
            }
            animator.SetBool("isDisplayed", isDisplayed);
            touchStart = null;
        }
    }

    public void OnClick()
    {
        Events.Instance.DispatchEvent("HideBattleSkillPanel", null);
        TurnManager.SelectSkill(skill);
    }
}
