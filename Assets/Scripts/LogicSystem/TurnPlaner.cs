using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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

        private TurnPlaner() {
            
        }

        private List<KeyValuePair<string, Plan>> plans = new List<KeyValuePair<string, Plan>>() ;
        public void AddPlan(string sheepName, Plan plan)
        {
            var pair = plans.FirstOrDefault(x => x.Value.Skill == plan.Skill);
            if (!pair.Equals(default(KeyValuePair<string, Plan>)))
            {
                Debug.Log("This skill was planned in this turn, swithing.");
                TurnManager.UpdateResource(0 - pair.Value.Skill.Cost);
                plans.Remove(plans.First(x => x.Value.Skill == plan.Skill));
            }
            plans.Add(new KeyValuePair<string, Plan>(sheepName, plan));
            Debug.Log("Added plan for " + sheepName);
        }

        public bool Execute()
        {
            //if (plans.Count < 4)
            //{
            //    Debug.Log("Not all sheep declared move");
            //    return false;
            //}
            foreach (var plan in plans)
            {
                plan.Value.Execute();
            }
            plans.Clear();
            return true;
        }

        public bool ContainsPlanForSheepSkill(string sheep, Skill skill)
        {
            return (plans.Where(x => x.Key == sheep).Where(x => x.Value.Skill == skill) != null);
        }

    }
}
