using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkillsMenuScript : MonoBehaviour {

    public WoolCounter WoolCounter;

    public SheepData[] SheepData;
    public int SheepNumber;

    public Image[] SheepIcon;

    public Button[] SkillsButtons;

    public GameObject Unlocker;
    public GameObject SkillDesc;
    public Text Name;
    public Text Description;
    public Text UnlockCost;
    public Text CurrentWool;
    public Button UnlockButton;
    public Image Icon;

    public static Color pushed = new Color(212, 212, 212, 255);
    public static Color unpushed = new Color(168, 168, 168, 255);
    public Color UnlockedColor = Color.white;
    public Color LockedColor = Color.gray; 

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
            SkillsButtons[i].image.sprite = skill.Icon;
            if (!skill.IsActive)
                SkillsButtons[i].image.color = LockedColor;
            else
                SkillsButtons[i].image.color = UnlockedColor;
        }
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
            Unlocker.SetActive(true);
            UnlockCost.text = skill.UnlockCost.ToString();
            CurrentWool.text = WoolCounter.WoolCount.ToString();
            UnlockButton.onClick.AddListener(() =>
            {
                if(WoolCounter.WoolCount>=skill.UnlockCost)
                {
                    skill.IsActive = true;
                    WoolCounter.WoolCount -= skill.UnlockCost;
                    Unlocker.SetActive(false);
                }
            });
        }

    }
}
