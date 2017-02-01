using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour, IEffect {

    public float SpellDuration;
    public float Delay;

    private float _speed;
    private Vector3 _direction;


    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position += actor.transform.position + new Vector3(0, 0.5f, 0);
        _direction = target.transform.position - actor.transform.position;
        _speed = _direction.magnitude / (SpellDuration - Delay);
    }

	void Update () {
        Delay -= Time.deltaTime;
        if(Delay < 0)
        {
            transform.position += _direction.normalized * _speed * Time.deltaTime;
        }
	}
}
