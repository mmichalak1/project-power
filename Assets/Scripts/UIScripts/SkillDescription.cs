using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour {

    public Text Name;
    public Text Description;
    public Text Cooldown;
    public Image Icon;



    public void LoadSkillData (Skill skill)
    {
        Name.text = skill.name;
        Description.text = skill.Description();
        Cooldown.text = "Cooldown: " + (skill.CooldownBase - 1);
        Icon.sprite = skill.Icon;

    }

}
