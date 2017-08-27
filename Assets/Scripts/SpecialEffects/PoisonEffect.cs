using System;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class PoisonEffect : MonoBehaviour, IDisappearAfterTurn, IOnTurnBegin
{
    public GameObject Source;
    public GameObject ParticleEffect;
    public int Damage = 3;
    public int Duration = 2;
    public void OnTurnBegin()
    {
        gameObject.GetComponent<IReciveDamage>().DealDamage(Damage, Source);
    }

    public void Tick()
    {
        if (--Duration < 1)
        {
            gameObject.GetComponent<EntityStatus>().PoisonEffects--;
            Destroy(ParticleEffect);
            Destroy(this);
        }
    }
}
