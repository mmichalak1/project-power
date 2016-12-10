using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LogicSystem
{
    public class Plan
    {
        GameObject _actor;
        GameObject _target;
        Skill _skill;
        public Skill Skill
        {
            get { return _skill; }
        }
        public GameObject Actor
        {
            get { return _actor; }
        }
        public GameObject Target
        {
            get { return _target; }
        }

        public Plan(GameObject actor, GameObject target, Skill skill)
        {
            _actor = actor;
            _skill = skill;
            _target = target;
        }

        public bool Execute()
        {
            if (_actor == null || _target == null)
                return false;
            if (!_actor.activeSelf || !_target.activeSelf)
            {
                Debug.Log(_actor.name + " can't attack because he or target is dead.");
                return false;
            }
            _skill.Action.Invoke(_actor, _target);

            return true;
        }

        public override string ToString()
        {
            return _actor.name + " uses " + _skill.name + " on " + _target.name;
        }

    }
}
