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

        private List<KeyValuePair<string, Plan>> plans = new List<KeyValuePair<string, Plan>>() ;
        public void AddPlan(string sheepName, Plan plan)
        {
            Debug.Log("Added plan for " + sheepName);
            plans.Add(new KeyValuePair<string, Plan>(sheepName, plan));
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
