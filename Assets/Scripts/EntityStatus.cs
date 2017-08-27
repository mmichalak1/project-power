using System;
using UnityEngine;
public class EntityStatus : MonoBehaviour
{
    [SerializeField]
    private bool stunned = false;
    [SerializeField]
    private bool poisoned = false;
    [SerializeField]
    private bool taunted = false;

    public bool Stunned { get { return stunned; } set { stunned = value; } }
    public bool Poisoned { get { return poisoned; } set { poisoned = value; } }
    public bool Taunted { get { return taunted; } set { taunted = value; } }

}
