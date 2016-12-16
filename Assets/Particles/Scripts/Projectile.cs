using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour, IEffect {

    public float SpellDuration;

    private float _speed;
    private Vector3 _direction;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position += actor.transform.position + new Vector3(0, 0.5f, 0);
        _direction = target.transform.position - actor.transform.position;
        _speed = _direction.magnitude / SpellDuration;
    }

	void Update () {
        transform.position += _direction.normalized * _speed * Time.deltaTime;
	}
}
