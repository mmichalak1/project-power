using UnityEngine;
using System.Text;
using UnityEngine.UI;


public class SkillsMenuScript : MonoBehaviour
{

    public WoolCounter WoolCounter;

    public EntityData[] SheepData;
    public SkillPanelScript[] SkillPanels;

    void Start()
    {
        LoadData(0);
    }

    public void LoadData(int sheepNumber)
    {
        for (int i = 1; i < 5; i++)
        {
            var skill = SheepData[sheepNumber].SheepSkills.Skills[i];
            SkillPanels[i-1].LoadSkillData(skill, SheepData[sheepNumber]);
        }
    }

}
