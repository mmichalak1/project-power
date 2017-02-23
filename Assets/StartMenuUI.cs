using UnityEngine;
using System.Collections;

public class StartMenuUI : MonoBehaviour
{

    public GameObject NewGameButton;
    public GameObject ContinueButton;
    public GameSaverScript GameSaver;
    public ResetSheeps SheepReset;
    public SwitchBetweenScenes SceneSwitch;

    // Use this for initialization
    void Start()
    {
        if (!GameSaver.IsSaveExisiting())
            ContinueButton.SetActive(false);
    }

    public void Continue()
    {
        GameSaver.LoadGame();
        SceneSwitch.LoadScene();
    }

    public void NewGame()
    {
        SheepReset.Reset();
        GameSaver.SaveGame();
        SceneSwitch.LoadScene();
    }

}
