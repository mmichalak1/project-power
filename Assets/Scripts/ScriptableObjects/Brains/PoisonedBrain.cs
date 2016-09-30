using UnityEngine;
using System.Collections;
using System;


namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Poisoned Brain", menuName = "Game/Brains/Poisoned Brain")]
    public class PoisonedBrain : AbstractBrain
    {

        public int Damage { get; set; }

        public void SetDuration(int duration)
        {
            Duration = duration;
        }


        public override void Initialize(GameObject[] targets)
        {

        }

        public override void Think(GameObject parent)
        {
            Debug.Log(parent.name + " takes " + Damage + " damage from poison.");
            parent.GetComponent<Interfaces.IReciveDamage>().DealDamage(Damage);
            base.Think(parent);
        }

    }
}

