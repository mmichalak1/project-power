using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.LogicSystem
{
    public class Plan
    {
        public Plan(GameObject actor, GameObject target, Action<GameObject, GameObject> action)
        {
            _actor = actor;
            _action = action;
            _target = target;
        }

        public void Execute()
        {
            _action.Invoke(_actor, _target);
        }

        GameObject _actor;
        GameObject _target;

        Action<GameObject, GameObject> _action;
    }
}
