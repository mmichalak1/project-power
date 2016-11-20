using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.LogicSystem
{
    public class TurnPlaner
    {
        public ActionBubble actionBubbleCleric;
        public ActionBubble actionBubbleMage;
        public ActionBubble actionBubbleRouge;
        public ActionBubble actionBubbleWarrior;
        public GameObject player;

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
            player = GameObject.Find("Player");
            List<RectTransform> listyOfAllImages = player.transform.GetComponentsInChildren<RectTransform>(true).ToList<RectTransform>();

            actionBubbleCleric = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleCleric").GetComponent(typeof(ActionBubble));
            actionBubbleMage = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleMage").GetComponent(typeof(ActionBubble));
            actionBubbleRouge = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleRouge").GetComponent(typeof(ActionBubble));
            actionBubbleWarrior = (ActionBubble)listyOfAllImages.Find(x => x.name == "actionBubbleWarrior").GetComponent(typeof(ActionBubble));
        }


        private List<KeyValuePair<string, Plan>> plans = new List<KeyValuePair<string, Plan>>() ;
        public void AddPlan(string sheepName, Plan plan)
        {
            var pair = plans.FirstOrDefault(x => x.Value.Skill == plan.Skill);
            if(ContainsPlanForSheepSkill(sheepName, plan.Skill))
            {
                Debug.Log("This skill was planned in this turn, switching.");
                plans.Remove(plans.First(x => x.Value.Skill == plan.Skill));
            } 
            EntityDataHolder sheepDataHolder = (EntityDataHolder)plan.Actor.GetComponent(typeof(EntityDataHolder));
            switch(sheepDataHolder.SheepData.SheepClass)
            {
                case EntityData.Class.Cleric:
                    actionBubbleCleric.TurnOn();
                    actionBubbleCleric.SetImage(plan.Skill.Icon);
                    break;
                case EntityData.Class.Mage:
                    actionBubbleMage.TurnOn();
                    actionBubbleMage.SetImage(plan.Skill.Icon);
                    break;
                case EntityData.Class.Rouge:
                    actionBubbleRouge.TurnOn();
                    actionBubbleRouge.SetImage(plan.Skill.Icon);
                    break;
                case EntityData.Class.Warrior:
                    actionBubbleWarrior.TurnOn();
                    actionBubbleWarrior.SetImage(plan.Skill.Icon);
                    break;
                default:
                    break;

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
            actionBubbleCleric.TurnOff();
            actionBubbleMage.TurnOff();
            actionBubbleRouge.TurnOff();
            actionBubbleWarrior.TurnOff();
            plans.Clear();
            return true;
        }

        public bool ContainsPlanForSheepSkill(string sheep, Skill skill)
        {
            var sheepsPlans = plans.Where(x => x.Key == sheep);
            var plan = sheepsPlans.FirstOrDefault(x => x.Value.Skill == skill);

            if (plan.Equals(default(KeyValuePair<string, Plan>)))
                return false;
            else
                return true;
        }

    }
}
