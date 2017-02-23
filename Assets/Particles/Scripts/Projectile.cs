using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpellOffsetActor))]
public class Projectile : MonoBehaviour, IEffect {

    public float SpellDuration;
    public float Delay;
    public SpellOffsetActor actorOffset;
    public TargetOffset tOffset;

    private float _speed;
    private Vector3 _direction;
    private float _distance;
    private float _distanceTraveled = 0;


    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position += actor.transform.position;
        Vector3 targetOffset = Vector3.zero;
        var targetingOffset = target.GetComponent<TargetingOffset>();
        if (targetingOffset != null)
        {
            switch (tOffset)
            {
                case TargetOffset.Belly: { targetOffset = targetingOffset.Belly; } break;
                case TargetOffset.Head: { targetOffset = targetingOffset.Head; } break;
            }
        }
        if (actorOffset != null)
            _direction = (target.transform.position + targetOffset) - (actor.transform.position + actorOffset.Offset);
        else
            _direction = target.transform.position - actor.transform.position;
        _distance = _direction.magnitude;
        _speed = _direction.magnitude / (SpellDuration - Delay);
    }

	void Update () {
        Delay -= Time.deltaTime;
        if(Delay < 0)
        {
            if (_distance > _distanceTraveled)
            {
                _distanceTraveled += (_direction.normalized * _speed * Time.deltaTime).magnitude;
                transform.position += _direction.normalized * _speed * Time.deltaTime;
            }
        }
	}
}
