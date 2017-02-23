using UnityEngine;
using System.Collections;
using System;

public class SpellOffsetTarget : MonoBehaviour, IEffect {

    public TargetOffset tOffset;

    public void SetUpAction(GameObject actor, GameObject target)
    {
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

        transform.position = target.transform.position;
        transform.position += targetOffset + new Vector3(0, 0.25f, 0);
    }
}
