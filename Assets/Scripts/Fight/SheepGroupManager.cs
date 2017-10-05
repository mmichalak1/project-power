using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;
using Assets.Scripts.Interfaces;
public class SheepGroupManager : MonoBehaviour {

    [SerializeField]
    ExplorationHolder holder;
    public GameObject LostPanel { get; set; }
    [SerializeField, Range(0, 1)]
    private float WoolGrowthMultiplier;
    GameObject ExplorationUI, BattleUI;
    [SerializeField]
    int SheepCount = 0;
    List<GameObject> sheep = new List<GameObject>();
    Events.MyEvent OnSetExplorationUI, OnSetBattleUI;

    public List<GameObject> Sheep { get { return sheep; } set { sheep = value; } }

    // Use this for initialization
    void Start() {

        OnSetBattleUI = new Events.MyEvent(x =>
        {
            BattleUI = x as GameObject;
        });
        OnSetExplorationUI = new Events.MyEvent(x =>
        {
            ExplorationUI = x as GameObject;
        });
        foreach (Transform child in transform)
            if (child.CompareTag("Sheep"))
                sheep.Add(child.gameObject);
        SheepCount = sheep.Count;
        foreach (var child in sheep)
        {
            Events.Instance.RegisterForEvent(child.name + "death", OnChildDeath);
        }
        Events.Instance.RegisterForEvent("BattleWon", OnBattleWon);
        Events.Instance.RegisterForEvent("SetExplorationUI", OnSetExplorationUI);
        Events.Instance.RegisterForEvent("SetBattleUI", OnSetBattleUI);
    }

    void OnChildDeath(object x)
    {
        SheepCount--;
        if (SheepCount == 0)
        {
            holder.GameResult = GameResult.Loss;
            Events.Instance.DispatchEvent("BattleLost", null);
            foreach (var item in gameObject.GetComponentsInChildren<EntityDataHolder>())
                EntityDataHolder.RevertItemsChange(item.SheepData);
            BattleUI.SetActive(false);
            ExplorationUI.SetActive(true);
            LostPanel.SetActive(true);
            Debug.Log("BattleLost");
        }
    }

    void OnBattleWon(object x)
    {
        foreach (var child in sheep)
        {
            child.GetComponent<ICanBeHealed>().HealToFull();
            child.GetComponentInChildren<MeshRenderer>().enabled = true;
            var effects = FindObjectsOfType<SC_SpellDuration>();
            foreach (var effect in effects)
            {
                effect.enabled = true;
            }
        }
        foreach (var data in GetComponentsInChildren<EntityDataHolder>())
        {
            var sheepData = data.SheepData;
            sheepData.SheepSkills.ResetCooldowns();
            //Calculate wool income for fight
            float income = sheepData.MaxWool * WoolGrowthMultiplier;
            data.SheepData.WoolGrowth += income;
        }

        Events.Instance.DispatchEvent("DestroyHealthBars", null);
    }

}
