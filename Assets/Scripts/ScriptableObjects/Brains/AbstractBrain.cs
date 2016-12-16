using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public abstract class AbstractBrain : ScriptableObject, IThink
    {
        [Range(0.0f, 1.0f), SerializeField, Tooltip("The higher the value the more important brain is, it will be processed before one with lower Importance")]
        protected float _importance = 0.5f;
        [SerializeField]
        private int _duration = -1;
        public GameObject ParticleEffect;

        public float Importance
        {
            get { return _importance; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public abstract void Initialize(GameObject[] targets);
        public virtual void Think(GameObject parent)
        {
            if (_duration > 0)
                _duration--;
        }
    }
}
