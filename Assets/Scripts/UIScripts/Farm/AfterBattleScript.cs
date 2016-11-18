using UnityEngine;
using UnityEngine.UI;
using Assets.LogicSystem;

public class AfterBattleScript : MonoBehaviour {

    public GameObject ResultsPanel;
    public Button[] SceneButtons;
    public EntityData[] Data;

	// Use this for initialization
	void Start () {
        switch (ExplorationResult.Instance.GameResult)
        {
            case Assets.Scripts.GameResult.Win:
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = true;
                ExplorationResult.Instance.LevelPlayed.OnLevelWon();
                break;
            case Assets.Scripts.GameResult.Loss:
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = false;
                ExplorationResult.Instance.LevelPlayed.OnLevelLost();
                EnableButtons();
                break;
            case Assets.Scripts.GameResult.Exited:
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = true;
                ExplorationResult.Instance.LevelPlayed.OnLevelLost();
                break;
            case Assets.Scripts.GameResult.None:
                ResultsPanel.SetActive(false);
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = false;
                EnableButtons();
                break;
            default:
                break;
        }
    }

    public void EnableButtons()
    {
        foreach (var button in SceneButtons)
        {
            button.interactable = true;
        }
    }
}
