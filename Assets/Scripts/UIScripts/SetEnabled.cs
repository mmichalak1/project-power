using UnityEngine;
using System.Collections;

public class SetEnabled : MonoBehaviour {

    public GameObject screenToView;
    public GameObject[] screensToHide;


	public void TurnOnScreen(){
        screenToView.SetActive(true);
        Debug.Log("Enabling" + screenToView.name);

        foreach (GameObject item in screensToHide)
            item.SetActive(false);
	}
}
