using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public abstract class AbstractBrain : ScriptableObject, IThink
    {
        [Range(0.0f, 1.0f), SerializeField]
        protected float _importance = 0.5f;
        [SerializeField]
        private int _duration = -1;

        public float Importance
        {
            get { return _importance; }
        }

        public int Duration
        {
            get { return _duration; }
            protected set { _duration = value; }
        }

        public abstract void Initialize(GameObject[] targets);
        public virtual void Think(GameObject parent)
        {
            if (_duration > 0)
                _duration--;
        }
    }
}
