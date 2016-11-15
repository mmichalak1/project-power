using UnityEngine;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour {

    public Text Name;
    public Text Description;
    public Image Icon;



    public void LoadSkillData (Skill skill)
    {
        Name.text = skill.name;
        Description.text = skill.Description();
        Icon.sprite = skill.Icon;
    }

}
