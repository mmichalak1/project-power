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

        private List<KeyValuePair<GameObject, Plan>> plans = new List<KeyValuePair<GameObject, Plan>>();

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

        public void AddPlan(GameObject entity, Plan plan)
        {
            if (ContainsPlanForSkill(plan.Skill, entity))
            {
                CancelPlan(plan.Skill);
            }
            TurnManager.UpdateResource(plan.Skill.Cost);
            plans.Add(new KeyValuePair<GameObject, Plan>(entity, plan));
            //Debug.Log("Added plan for " + entity.name);
        }

        public void CancelPlan(Skill cancelledSkill)
        {
            TurnManager.UpdateResource(-cancelledSkill.Cost);
            var plan = plans.First(x => x.Value.Skill == cancelledSkill);
            plans.Remove(plan);
            if (!ContainsPlanForEntity(plan.Key))
            {
                var bubble = plan.Key.GetComponentInChildren<ActionBubble>();
                if (bubble != null)
                    bubble.TurnOff();
            }

        }

        public void Reset()
        {
            plans.Clear();
        }

        public bool ContainsPlanForSkill(Skill skill, GameObject entity)
        {
            var plan = plans.FirstOrDefault(x => x.Key == entity && x.Value.Skill == skill);

            if (plan.Equals(default(KeyValuePair<GameObject, Plan>)))
                return false;
            else
                return true;
        }

        public bool ContainsPlanForEntity(GameObject entity)
        {
            var plan = plans.FirstOrDefault(x => x.Key == entity);

            if (plan.Equals(default(KeyValuePair<GameObject, Plan>)))
                return false;
            else
                return true;
        }

    }
}
