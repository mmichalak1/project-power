using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public abstract class AbstractBrain : ScriptableObject, IThink
    {
        public abstract void Initialize(GameObject[] targets);
        public abstract void Think(GameObject parent);
    }
}
