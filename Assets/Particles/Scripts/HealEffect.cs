using UnityEngine;
using System.Collections;
using System;

public class HealEffect : MonoBehaviour, IEffect {

    public float LifeTime = 5.0f;

    private float time = 0.0f;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position = target.transform.position;
        transform.position += transform.up *  0.5f + transform.forward * 0.0f + transform.right * 0.1f;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
	    if(LifeTime < time)
        {
            Destroy(gameObject);
        }
	}
}
