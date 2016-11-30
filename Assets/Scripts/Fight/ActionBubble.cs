using UnityEngine;
using UnityEngine.UI;

public class ActionBubble : MonoBehaviour {

    public Image[] skillImage;
    private Skill[] skills = new Skill[3];
    private int whichIcon = 0;
    public Sprite imageForNull;
    public void SetSkill(Skill skill)
    {
        if(whichIcon<3)
        {
            skillImage[whichIcon].sprite = skill.Icon;
            skills[whichIcon] = skill;
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
        gameObject.SetActive(true);
        //Debug.Log("Enabling" + this.gameObject.name);
    }
    public void TurnOff()
    {
        Clear();
        gameObject.SetActive(false);
        //Debug.Log("Disabling" + this.gameObject.name);
    }
}
