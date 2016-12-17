using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.LogicSystem
{
    public class TurnPlaner
    {
        private static TurnPlaner _instance = null;
        public static TurnPlaner Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TurnPlaner();
                return _instance;
            }
        }

        private TurnPlaner()
        {

        }

        private List<KeyValuePair<string, Plan>> plans = new List<KeyValuePair<string, Plan>>();

        public Queue<Plan> Queue
        {
            get
            {
                var queue = new Queue<Plan>();
                for (int i = 0; i < plans.Count; i++)
                {
                    queue.Enqueue(plans[i].Value);
                }
                return queue;
            }
        }

        public void AddPlan(string entityName, Plan plan)
        {
            if (ContainsPlanForSkill(plan.Skill, entityName))
            {
                CancelPlan(plan.Skill);
            }
            plans.Add(new KeyValuePair<string, Plan>(entityName, plan));
            Debug.Log("Added plan for " + entityName);
        }

        public void CancelPlan(Skill cancelledSkill)
        {
            plans.Remove(plans.First(x => x.Value.Skill == cancelledSkill));
        }

        public void Reset()
        {
            plans.Clear();
        }

        public bool ContainsPlanForSkill(Skill skill, string entityName)
        {
            var plan = plans.FirstOrDefault(x => x.Key==entityName && x.Value.Skill == skill);

            if (plan.Equals(default(KeyValuePair<string, Plan>)))
                return false;
            else
                return true;
        }

    }
}
