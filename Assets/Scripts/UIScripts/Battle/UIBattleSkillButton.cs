using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Assets.LogicSystem;

public class UIBattleSkillButton : MonoBehaviour
{
    public Image SkillIcon;
    public Text CooldownCounter;

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
            SkillIcon.sprite = skill.Icon;
            UpdateState();
        }
    }

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Events.Instance.RegisterForEvent("HideBattleSkillPanel", Hide);
    }

    void UpdateState()
    {
        if (skill != null)
        {
            if(skill.Cooldown <= 0)
            {
                SkillIcon.raycastTarget = true;
                SkillIcon.color = Color.white;
            }
            else
            {
                SkillIcon.raycastTarget = false;
                SkillIcon.color = Color.grey;
                CooldownCounter.enabled = true;
                CooldownCounter.text = skill.Cooldown.ToString();
            }
        }
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
        isDisplayed = true;
        animator.SetBool("isDisplayed", isDisplayed);
        SystemAccessor.GetSystem<TurnManagerInteface>().SelectSkill(skill);
    }
}
