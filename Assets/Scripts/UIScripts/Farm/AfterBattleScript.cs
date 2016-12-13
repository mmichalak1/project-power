using UnityEngine;
using UnityEngine.UI;

public class AfterBattleScript : MonoBehaviour {

    public ExplorationHolder holder;
    public GameObject ResultsPanel;
    public Button[] SceneButtons;
    public EntityData[] Data;

	// Use this for initialization
	void Start () {
        switch (holder.GameResult)
        {
            case GameResult.Win:
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = true;
                holder.LevelPlayed.OnLevelWon();
                break;
            case GameResult.Loss:
                ResultsPanel.GetComponent<AfterBattlePanelScript>().addingExperience = true;
                holder.LevelPlayed.OnLevelLost();
                break;
            case GameResult.None:
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
