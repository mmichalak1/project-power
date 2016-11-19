using UnityEngine;
using System.Collections;

public class SetEnabledMany : MonoBehaviour
{

    public GameObject[] screenToView;
    public GameObject[] screensToHide;


    public void TurnOnScreen()
    {
        foreach (GameObject item in screenToView)
        {
            item.SetActive(true);
            Debug.Log("Enabling" + item.name);
        }

        foreach (GameObject item in screensToHide)
            item.SetActive(false);
    }
}
