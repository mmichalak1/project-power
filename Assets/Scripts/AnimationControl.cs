using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move()
    {
        anim.SetBool("Walk", true);
    }
    public void Stop()
    {
        anim.SetBool("Walk", false);
    }
    public void SkillOn(int i)
    {
        anim.SetBool("Skill" + i, true);
    }
    public void SkillOff(int i)
    {
        anim.SetBool("Skill" + i, false);
    }
}
