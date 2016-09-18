using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName ="TauntedBrain", menuName = "Game/Brains/Taunted Brain")]
    public class TauntedBrain : AbstractBrain {

        [HideInInspector]
        public GameObject Target;
        public int MyDamage = 0;
        public override void Initialize(GameObject[] targets)
        {
        }

        public override void Think(GameObject parent)
        {
            Debug.Log(parent.name + " attacks " + Target.name + " because he insulted his mother!");
            parent.GetComponent<AttackController>().BreakTurn = true;
            Target.GetComponent<Interfaces.IReciveDamage>().DealDamage(MyDamage);
            base.Think(parent);
        }
    }
}

