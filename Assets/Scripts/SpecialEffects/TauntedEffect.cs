using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
using System;

public class TauntedEffect : MonoBehaviour, IDisappearAfterTurn
{
    private int _duration = 3;
    private GameObject _particleEffect;
    public int Duration { get { return _duration; } set { _duration = value; } }
    public GameObject ParticleEffect { get { return _particleEffect; } set { _particleEffect = value; } }
    public GameObject Target { get; set; }

    public void Tick()
    {
        if (--Duration < 1)
        {
            gameObject.GetComponent<EntityStatus>().Taunted = false;
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Destroy(ParticleEffect);
    }
}
