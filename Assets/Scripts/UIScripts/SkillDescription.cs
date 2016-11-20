using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour {

    public Text Name;
    public Text Description;
    public Text Cooldown;
    public Text Cost;
    public Image Icon;



    public void LoadSkillData (Skill skill)
    {
        Name.text = skill.name;
        Description.text = skill.Description();
        Cooldown.text = "Cooldown: " + (skill.CooldownBase - 1);
        Cost.text = "Cost: : " + skill.Cost;
        Icon.sprite = skill.Icon;

    }

}
