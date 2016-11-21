using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName ="TauntedBrain", menuName = "Game/Brains/Taunted Brain")]
    public class TauntedBrain : AbstractBrain {

        [HideInInspector]
        public GameObject Target;

        private int _myRealDamage;
        public override void Initialize(GameObject[] targets)
        {
        }

        public override void Think(GameObject parent)
        {
            _myRealDamage = parent.GetComponent<AttackController>().Damage;
            var debuffs = parent.GetComponents<DamageDebuff>();
             
            if(debuffs.Length != 0)
            {
                foreach (var item in debuffs)
                {
                    _myRealDamage -= (_myRealDamage * item.DebuffValue) / 100;
                }
            }

            Debug.Log(parent.name + " attacks " + Target.name + " because he insulted his mother!");
            parent.GetComponent<AttackController>().BreakTurn = true;
            Target.GetComponent<Interfaces.IReciveDamage>().DealDamage(_myRealDamage, parent);
            base.Think(parent);
        }
    }
}

