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

        private List<Plan> plans = new List<Plan>();
        private PlanComparator comparator = new PlanComparator();

        public Queue<Plan> Queue
        {
            get
            {
                var queue = new Queue<Plan>();
                for (int i = 0; i < plans.Count; i++)
                {
                    queue.Enqueue(plans[i]);
                }
                return queue;
            }
        }

        public void AddPlan(Plan plan)
        {
            plans.Add(plan);
            //Debug.Log("Added plan for " + entity.name);
        }

        public void CancelPlan(Plan cancelledPlan)
        {
            plans.Remove(cancelledPlan);
        }

        public void Reset()
        {
            plans.Clear();
        }

        public bool ContainsPlan(Plan plan)
        {
            return plans.Contains(plan, comparator);
        }

        public bool ContainsPlanWithSkill(Skill skill)
        {
            return plans.Any(x => x.Skill == skill);
        }

        public bool ContainsPlanForActor(GameObject actor)
        {
            return plans.Any(x => x.Actor == actor);
        }

    }
}
