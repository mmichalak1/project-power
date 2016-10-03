using UnityEngine;
using System.Collections;
using System;

public class GiveAwayDamage : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    [HideInInspector]
    public int Duration = 1;

    [Range(10, 100)]
    public int DamagePercentReturned = 20;

    public void FightBack(int sourceDamage, GameObject target)
    {
        if (target == gameObject)
            return;
        Debug.Log(gameObject.name + "'s shield returns " + (sourceDamage * DamagePercentReturned) / 100 + " damage to " + target.name);
        target.GetComponent<Assets.Scripts.Interfaces.IReciveDamage>().DealDamage((sourceDamage * DamagePercentReturned) / 100);
    }

    public void Tick()
    {
        if (--Duration == 0)
            Destroy(this);
    }


}
