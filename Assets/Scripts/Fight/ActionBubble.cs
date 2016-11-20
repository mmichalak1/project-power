using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionBubble : MonoBehaviour {

    public GameObject actionView;
    public Image[] skillImage;
    private int whichIcon = 0;
    public void SetImage(Sprite sprite)
    {
        if(whichIcon<3)
        {
            skillImage[whichIcon].sprite = sprite;
            whichIcon++;
        }
        
    }
    public void Clear()
    {
        whichIcon = 0;
        foreach (Image item in skillImage)
            item.sprite = null;
    }
    public void TurnOn()
    {
        actionView.SetActive(true);
        Debug.Log("Enabling" + actionView.name);
    }
    public void TurnOff()
    {
        Clear();
        actionView.SetActive(false);
        Debug.Log("Disabling" + actionView.name);
    }
}
