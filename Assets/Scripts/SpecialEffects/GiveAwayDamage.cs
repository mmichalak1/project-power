using UnityEngine;
using System.Collections;
using System;

public class GiveAwayDamage : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    [HideInInspector]
    public int Duration = 1;

    [Range(10, 100)]
    public int DamagePercentReturned = 20;

    public int BaseDamage = 0;

    public GameObject ParticleEffect;

    public void FightBack(int sourceDamage, GameObject target)
    {
        if (target == gameObject)
            return;
        Debug.Log(gameObject.name + "'s shield returns " + BaseDamage + " + " + (sourceDamage * DamagePercentReturned) / 100 + " damage to " + target.name);
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage(BaseDamage + (sourceDamage * DamagePercentReturned) / 100);
    }

    public void Tick()
    {
        if (--Duration == 0)
        {
            if (ParticleEffect != null)
                ParticleEffect.GetComponent<SC_SpellDuration>().enabled = true;
            Destroy(this);
        }
    }


}
