using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillPanelScript : MonoBehaviour {

    public Text Name;
    public Text Description;
    public Text Cooldown;
    public Text Cost;
    public Text ResourceCost;
    public Text MessageText;
    public Button UnlockButton;
    public WoolCounter WoolCounter;
    public Image Icon;

    private Skill skill;
    private EntityData sheep;


    public void LoadSkillData(Skill skill, EntityData sheep)
    {
        this.skill = skill;
        this.sheep = sheep;
        Name.text = skill.name;
        Description.text = skill.Description();
        Cooldown.text = (skill.CooldownBase - 1).ToString();
        Cost.text = skill.UnlockCost.ToString();
        ResourceCost.text = skill.ResourceCost.ToString();
        Icon.sprite = skill.Icon;
        UnlockButton.interactable = canBeUnlocked();
    }

    public void UnlockAction()
    {
        if (WoolCounter.WoolCount >= skill.UnlockCost)
            if (skill != null)
            {
                skill.IsActive = true;
                WoolCounter.WoolCount -= skill.UnlockCost;
                UnlockButton.interactable = canBeUnlocked();
                //LoadData(SheepNumber);
            }
    }

    private bool canBeUnlocked()
    {
        if (sheep.Level < skill.RequiredSheepLevel)
        {
            MessageText.color = Color.red;
            MessageText.text = "Required level: " + skill.RequiredSheepLevel;
            MessageText.gameObject.SetActive(true);
            return false;
        }
        if (skill.IsActive)
        {
            MessageText.color = Color.green;
            MessageText.text = "Already unlocked";
            MessageText.gameObject.SetActive(true);
            return false;
        }
        MessageText.gameObject.SetActive(false);
        return true;

    }
}
