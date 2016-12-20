using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Tutorial Tutorial;
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
	
	// Update is called once per frame
	void Update () {
	
	}


    public void FinishedReading()
    {
        Tutorial.WasSeen = true;
        Destroy(gameObject);
    }
}
