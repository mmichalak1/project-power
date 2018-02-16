using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Tutorial Tutorial;
    public GameSaverScript GameSaverScript;
    public Text Title;
    public Text Text;


	// Use this for initialization
	void Start () {
        if (Tutorial.WasSeen)
        {
            FinishedReading();
        }
        else
        {
            Title.text = Tutorial.name;
            Text.text = Tutorial.Text;
        }

	}

    public void FinishedReading()
    {
        Tutorial.WasSeen = true;
        this.GameSaverScript.SaveGame();
        Destroy(gameObject);
    }
}
