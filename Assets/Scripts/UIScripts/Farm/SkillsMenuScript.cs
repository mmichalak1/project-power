using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkillsMenuScript : MonoBehaviour
{

    public WoolCounter WoolCounter;

    public EntityData[] SheepData;
    public int SheepNumber;

    public Image SelectedSheepIcon;
    public Button[] SkillsButtons;

    public GameObject Unlocker;
    public GameObject SkillDesc;
    public Text Name;
    public Text Description;
    public Text UnlockCost;
    public Button UnlockButton;
    public Image Icon;
    public Text Cooldown;
    public Text Cost;

    public static Color pushed = new Color(212, 212, 212, 255);
    public static Color unpushed = new Color(168, 168, 168, 255);
    public Color UnlockedColor = Color.white;
    public Color LockedColor = Color.gray;

    private Skill currentlyPickedSkill;

    void Start()
    {
        LoadData(0);
    }

    public void LoadData(int SheepNumber)
    {
        this.SheepNumber = SheepNumber;
        SelectedSheepIcon.sprite = SheepData[SheepNumber].Portrait;
        for (int i = 0; i < 4; i++)
        {
            var skill = SheepData[SheepNumber].SheepSkills.Skills[i];
            skill.Initialize(SheepData[SheepNumber]);
            SkillsButtons[i].image.sprite = skill.Icon;
            if (!skill.IsActive)
                SkillsButtons[i].image.color = LockedColor;
            else
                SkillsButtons[i].image.color = UnlockedColor;
        }
    }

    public void ButtonClick(int SheepButton)
    {
        Unlocker.SetActive(false);
        currentlyPickedSkill = SheepData[SheepNumber].SheepSkills.Skills[SheepButton];
        SkillDesc.SetActive(true);
        Name.text = currentlyPickedSkill.Name;
        Description.text = currentlyPickedSkill.Description();
        Icon.sprite = currentlyPickedSkill.Icon;
        Cooldown.text = currentlyPickedSkill.CooldownBase.ToString();
        Cost.text = currentlyPickedSkill.Cost.ToString();
        if (!currentlyPickedSkill.IsActive)
        {
            Unlocker.SetActive(true);
            UnlockCost.text = currentlyPickedSkill.UnlockCost.ToString();
        }
    }


    public void UnlockAction()
    {
        if (WoolCounter.WoolCount >= currentlyPickedSkill.UnlockCost)
            if (currentlyPickedSkill != null)
            {
                currentlyPickedSkill.IsActive = true;
                WoolCounter.WoolCount -= currentlyPickedSkill.UnlockCost;
                Unlocker.SetActive(false);
                LoadData(SheepNumber);
            }
    }
}
