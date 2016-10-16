using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkillsMenuScript : MonoBehaviour {

    public SheepData[] SheepData;
    public int SheepNumber;

    public Image[] SheepIcon;

    public Button[] SheepButton;

    public GameObject SkillDesc;
    public Text Name;
    public Text Description;
    public Image Icon;

    public static Color pushed = new Color(0.83F, 0.83F, 0.83F, 1.0F);
    public static Color unpushed = new Color(0.73F, 0.73F, 0.73F, 1.0F);

    void Start()
    {
        LoadData(0);
    }

    public void LoadData(int SheepNumber)
    {
        this.SheepNumber = SheepNumber;
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
    public void ButtonClick(int SheepButton)
    {
        SkillDesc.SetActive(true);
        Name.text = SheepData[this.SheepNumber].SheepSkills.Skills[SheepButton].Name;
        Description.text = SheepData[this.SheepNumber].SheepSkills.Skills[SheepButton].Description;
        Icon.sprite = SheepData[this.SheepNumber].SheepSkills.Skills[SheepButton].Icon;
    }

    public void ButtonExit()
    {
        SkillDesc.SetActive(false);
    }
}
