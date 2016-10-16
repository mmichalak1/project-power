using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkillsMenuScript : MonoBehaviour {

    public SheepData[] SheepData;

    public Image[] SheepIcon;

    public Button[] SheepButton;

    public static Color pushed = new Color(0.83F, 0.83F, 0.83F, 1.0F);
    public static Color unpushed = new Color(0.73F, 0.73F, 0.73F, 1.0F);

    void Start()
    {
        LoadData(0);
    }

    public void LoadData(int SheepNumber)
    {
        for(int i = 0; i<4; i++)
        {
            SheepButton[i].image.sprite = SheepData[SheepNumber].SheepSkills.Skills[i].Icon;
        }
        ColorChange(SheepNumber);
    }

    public void ColorChange(int SheepNumber)
    {
        foreach (Image image in SheepIcon)
        {
            image.color = unpushed;
        }
        SheepIcon[SheepNumber].color = pushed;

    }
}
