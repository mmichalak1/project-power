using UnityEngine;
using System.Collections;
using System;

public class HealEffect : MonoBehaviour, IEffect {

    [Header("Config")]
    public float spellDuration;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position = target.transform.position;
        transform.position += transform.up *  0.5f + transform.forward * 0.0f + transform.right * 0.1f;
    }

    void Start()
    {
        Destroy(gameObject, spellDuration);
    }

}
