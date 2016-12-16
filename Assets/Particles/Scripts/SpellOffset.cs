﻿using UnityEngine;
using System.Collections;
using System;

public class SpellOffset : MonoBehaviour, IEffect
{
    public Vector3 Offset;

    public void SetUpAction(GameObject actor, GameObject target)
    {
        transform.position = target.transform.position;
        transform.position += Offset;
    }
}
