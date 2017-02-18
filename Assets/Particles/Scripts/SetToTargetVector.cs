using UnityEngine;
using System.Collections;
using System;

public class SetToTargetVector : MonoBehaviour, IEffect {


    public void SetUpAction(GameObject actor, GameObject target)
    {
        var toTarget = target.transform.position - actor.transform.position;
        var rotation = Quaternion.FromToRotation(Vector3.up, toTarget);
        gameObject.transform.rotation = rotation;
        int i = 0;
    }

}
