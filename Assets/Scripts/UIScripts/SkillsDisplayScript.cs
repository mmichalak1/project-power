using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsDisplayScript : MonoBehaviour {

    public List<SkillPanelScript> SkillPanels;
    public List<EntityData> Sheeps;
    public PlayerData playerData;

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < 4; i++)
        {
            SkillPanels[i].LoadSkillData(Sheeps[0].SheepSkills.Skills[i + 1], Sheeps[0], playerData);
        }
	}
	
	public void LoadData(int sheep)
    {
        for (int i = 0; i < 4; i++)
        {
            SkillPanels[i].LoadSkillData(Sheeps[sheep].SheepSkills.Skills[i + 1], Sheeps[sheep], playerData);
        }
    }
}
