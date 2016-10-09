using UnityEngine;
using System.Collections;
using System;

public class DamageReductor : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    public int Duration = 1;
    public int DamageReduced = 10;


    public void Tick()
    {
        if (--Duration == 0)
            Destroy(this);
    }   
}
