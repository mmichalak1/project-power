using UnityEngine;
using UnityEngine.UI;

public class ActionBubble : MonoBehaviour {

    public void TurnOn()
    {
        gameObject.SetActive(true);
        //Debug.Log("Enabling" + this.gameObject.name);
    }
    public void TurnOff()
    {
        gameObject.SetActive(false);
        //Debug.Log("Disabling" + this.gameObject.name);
    }
}
