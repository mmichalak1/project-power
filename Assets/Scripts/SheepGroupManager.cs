using UnityEngine;
using System.Collections.Generic;
using Assets.LogicSystem;
using Assets.Scripts.Interfaces;
public class SheepGroupManager : MonoBehaviour {

    [SerializeField]
    private GameObject LostPanel;
    GameObject ExplorationUI, BattleUI;
    int SheepCount = 0;
    List<Transform> sheep = new List<Transform>();

    // Use this for initialization
    void Start() {
        foreach (Transform child in transform)
            if (child.CompareTag("Sheep"))
                sheep.Add(child);
        SheepCount = sheep.Count;
        foreach (var child in sheep)
            Events.Instance.RegisterForEvent(child.name + "death", x =>
            {
                SheepCount--;
                if (SheepCount == 0)
                {
                    ExplorationResult.Instance.GameResult = Assets.Scripts.GameResult.Loss;
                    Events.Instance.DispatchEvent("BattleLost", null);
                    BattleUI.SetActive(false);
                    ExplorationUI.SetActive(true);
                    LostPanel.SetActive(true);
                    Debug.Log("BattleLost");
                }
            });
        Events.Instance.RegisterForEvent("BattleWon", x =>
        {
            foreach (var child in sheep)
            {
                child.gameObject.GetComponent<ICanBeHealed>().HealToFull();
                child.gameObject.SetActive(true);
            }
            foreach (var data in GetComponentsInChildren<SheepDataHolder>())
                data.SheepData.SheepSkills.ResetCooldowns();
            Events.Instance.DispatchEvent("DestroyHealthBars", null);
                
        });
        Events.Instance.RegisterForEvent("SetExplorationUI", x =>
        {
            ExplorationUI = x as GameObject;
        });
        Events.Instance.RegisterForEvent("SetBattleUI", x =>
        {
            BattleUI = x as GameObject;
        });
    }

}
