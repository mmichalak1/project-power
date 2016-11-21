using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionBubble : MonoBehaviour {

    public Image[] skillImage;
    private int whichIcon = 0;
    public Sprite imageForNull;
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
            item.sprite = imageForNull;
    }
    public void TurnOn()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Enabling" + this.gameObject.name);
    }
    public void TurnOff()
    {
        Clear();
        this.gameObject.SetActive(false);
        Debug.Log("Disabling" + this.gameObject.name);
    }
}
