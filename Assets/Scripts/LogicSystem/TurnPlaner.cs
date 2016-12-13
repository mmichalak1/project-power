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

        public void AddPlan(string sheepName, Plan plan)
        {
            if (ContainsPlanForSkill(plan.Skill))
            {
                CancelPlan(plan.Skill);
            }
            plans.Add(new KeyValuePair<string, Plan>(sheepName, plan));
            Debug.Log("Added plan for " + sheepName);
        }

        public void CancelPlan(Skill cancelledSkill)
        {
            plans.Remove(plans.First(x => x.Value.Skill == cancelledSkill));
        }

        public void Reset()
        {
            plans.Clear();
        }

        public bool ContainsPlanForSkill(Skill skill)
        {
            var plan = plans.FirstOrDefault(x => x.Value.Skill == skill);

            if (plan.Equals(default(KeyValuePair<string, Plan>)))
                return false;
            else
                return true;
        }

    }
}
