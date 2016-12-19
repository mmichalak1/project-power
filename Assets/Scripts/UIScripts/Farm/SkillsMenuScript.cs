using UnityEngine;
using System.Text;
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
    public Text RequiredLevel;
    public Button UnlockButton;
    public Image Icon;
    public Text Cooldown;
    public Text Cost;

    public Color UnlockedColor = Color.white;
    public Color LockedColor = Color.gray;
    public Color LockedByLvlColor;

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

            if (skill.RequiredSheepLevel > SheepData[SheepNumber].Level)
            {
                SkillsButtons[i].image.color = LockedByLvlColor;
            }
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
            UnlockButton.interactable = true;
            if (SheepData[SheepNumber].Level<currentlyPickedSkill.RequiredSheepLevel)
            {
                UnlockButton.interactable = false;
                RequiredLevel.gameObject.SetActive(true);
                RequiredLevel.text = "Requried level: " + currentlyPickedSkill.RequiredSheepLevel;
            }
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
