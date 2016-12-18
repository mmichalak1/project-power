using UnityEngine;
using System.Collections;
using System;

public class Storm_OnHit : MonoBehaviour, IEffect {

    [SerializeField]
    private GameObject Lightning;
    public Light AmbientLight;
    public float DelayTime;
    public float Ratio;
    private float deltaTime = 0;

    void Start()
    {
        AmbientLight = GameObject.FindGameObjectWithTag("AmbientLight").GetComponent<Light>();
    }

    public void SetUpAction(GameObject actor, GameObject target)
    {
        var enemies = target.GetComponentInParent<WolfGroupManager>().enemies;
        foreach(var enemy in enemies)
        {
            GameObject go = Instantiate(Lightning, enemy.transform.position, Quaternion.identity) as GameObject;
            go.transform.parent = enemy.transform;

        }
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        if(DelayTime < deltaTime)
        {
            Brightening();
        }
    }

    void Brightening()
    {
        if(AmbientLight.intensity <= 0.8f)
        {
            AmbientLight.intensity += Time.deltaTime * Ratio;
        }
    }
}
