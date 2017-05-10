using UnityEngine;
using System.Collections;
using System;
using Assets.LogicSystem;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName ="TauntedBrain", menuName = "Game/Brains/Taunted Brain")]
    public class TauntedBrain : AbstractBrain {

        [HideInInspector]
        public GameObject Target;
        [SerializeField]
        private Skill AttackSkill;

        private int _myRealDamage;
        public override void Initialize(GameObject[] target)
        {
            AttackSkill.Initialize();
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
            AttackSkill.Power = _myRealDamage;
            TurnPlaner.Instance.AddPlan(new Plan(parent, Target, AttackSkill));
            base.Think(parent);
        }
    }
}

