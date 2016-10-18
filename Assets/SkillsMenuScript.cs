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
    public Text UnlockLabel;
    public Text UnlockCost;

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
            var skill = SheepData[SheepNumber].SheepSkills.Skills[i];
            SheepButton[i].image.sprite = skill.Icon;
            if (!skill.IsActive)
                SheepButton[i].image.color = Color.grey;
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
        var skill = SheepData[SheepNumber].SheepSkills.Skills[SheepButton];
        SkillDesc.SetActive(true);
        Name.text = skill.Name;
        Description.text = skill.Description;
        Icon.sprite = skill.Icon;
        if(!skill.IsActive)
        {
            UnlockLabel.gameObject.SetActive(true);
            UnlockCost.gameObject.SetActive(true);
        }

    }

    public void ButtonExit()
    {
        SkillDesc.SetActive(false);
    }
}
