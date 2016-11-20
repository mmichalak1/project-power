using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LogicSystem
{
    public class Plan
    {
        public Plan(GameObject actor, GameObject target, Skill skill)
        {
            _actor = actor;
            _skill = skill;
            _target = target;
        }

        public void Execute()
        {
            _skill.Action.Invoke(_actor, _target);
        }

        public Skill Skill
        {
            get { return _skill; }
        }
        public GameObject Actor
        {
            get { return _actor; }
        }
        GameObject _actor;
        GameObject _target;

        Skill _skill;
    }
}
