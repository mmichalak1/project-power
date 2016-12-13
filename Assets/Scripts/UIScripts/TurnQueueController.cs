using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TurnQueueController : MonoBehaviour
{

    public GameObject SkillPrefab;
    private Dictionary<Skill, GameObject> _plannedSkills = new Dictionary<Skill, GameObject>();



    public void AddSkill(Skill skill)
    {
        if (_plannedSkills.ContainsKey(skill))
            _plannedSkills.Remove(skill);

        GameObject go = Instantiate(SkillPrefab) as GameObject;
        go.transform.SetParent(gameObject.transform, false);
        go.GetComponent<Image>().sprite = skill.Icon;
        _plannedSkills.Add(skill, go);

    }

    public void RemoveSkill(Skill skill)
    {
        _plannedSkills.Remove(skill);
    }

    public void Clear()
    {
        foreach (var item in _plannedSkills)
        {
            Destroy(item.Value);
        }
        _plannedSkills.Clear();
    }

}
