using UnityEngine;
using System.Collections;
using System;

public class SpellOffsetActor : MonoBehaviour, IEffect
{
    public Vector3 Offset;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position = actor.transform.position;
        transform.position += Offset;
    }
}
