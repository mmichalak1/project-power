using System;
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

        private Dictionary<string, Plan> plans = new Dictionary<string, Plan>() ;
        public void AddPlan(string sheepName, Plan plan)
        {
            Debug.Log("Added plan for " + sheepName);
            if (plans.ContainsKey(sheepName))
                plans.Remove(sheepName);
            plans.Add(sheepName, plan);
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

    }
}
