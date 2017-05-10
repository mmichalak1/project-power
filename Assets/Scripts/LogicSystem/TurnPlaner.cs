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
            if (ContainsPlan(plan))
            {
                CancelPlan(plan);
            }
            TurnManager.UpdateResource(plan.Skill.Cost);
            plans.Add(plan);
            //Debug.Log("Added plan for " + entity.name);
        }

        public void CancelPlan(Plan cancelledPlan)
        {
            TurnManager.UpdateResource(-cancelledPlan.Skill.Cost);
            plans.Remove(cancelledPlan);
            if (!ContainsPlanForActor(cancelledPlan.Actor))
            {
                var bubble = cancelledPlan.Actor.GetComponentInChildren<ActionBubble>();
                if (bubble != null)
                    bubble.TurnOff();
            }

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
