using UnityEngine;
using System.Collections;
using System;

public class UIBattleResourcePanel : MonoBehaviour {

    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    
    internal void Activate(bool isDisplayed)
    {
        animator.SetBool("isDisplayed", isDisplayed);
    }
}
