using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpellOffsetActor))]
public class SetToTargetVector : MonoBehaviour, IEffect {

    public SpellOffsetActor actorOffset;
    public TargetOffset tOffset;
    private Vector3 _direction;

    public void SetUpAction(GameObject actor, GameObject target)
    {

        Vector3 targetOffset = Vector3.zero;
        var targetingOffset = target.GetComponent<TargetingOffset>();
        if (targetingOffset != null)
        {
            switch(tOffset)
            {
                case TargetOffset.Belly: { targetOffset = targetingOffset.Belly; } break;
                case TargetOffset.Head: { targetOffset = targetingOffset.Head; } break;
            }
        }
            

        if (actorOffset != null)
            _direction = (target.transform.position + targetOffset) - (actor.transform.position + actorOffset.Offset);
        else
            _direction = target.transform.position - actor.transform.position;
        var rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        gameObject.transform.rotation = rotation;
        int i = 0;
    }

}
