using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpellOffsetActor))]
public class SetToTargetVector : MonoBehaviour, IEffect {

    public SpellOffsetActor actorOffset;

    private Vector3 _direction;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        if (actorOffset != null)
            _direction = target.transform.position - (actor.transform.position + actorOffset.Offset);
        else
            _direction = target.transform.position - actor.transform.position;
        var rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        gameObject.transform.rotation = rotation;
        int i = 0;
    }

}
