using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class TurnQueueController : MonoBehaviour
{

    public GameObject SkillPrefab;
    private Dictionary<Skill, GameObject> _plannedSkills = new Dictionary<Skill, GameObject>();



    public void AddSkill(Skill skill)
    {
        if (_plannedSkills.ContainsKey(skill))
            _plannedSkills.Remove(skill);

        var go = Instantiate(SkillPrefab);
        go.transform.SetParent(gameObject.transform, false);
        go.GetComponent<Image>().sprite = skill.Icon;
        go.GetComponent<Button>().onClick.AddListener(() => { CancelSkill(go); });
        _plannedSkills.Add(skill, go);

    }


    public void CancelSkill(GameObject go)
    {
        var skill = _plannedSkills.First(x => x.Value == go).Key;
        _plannedSkills.Remove(skill);
        Destroy(go);
        TurnPlaner.Instance.CancelPlan(skill);
    }

    public void CancelSkill(Skill skill)
    {
        var go = _plannedSkills[skill];
        _plannedSkills.Remove(skill);
        Destroy(go);
        TurnPlaner.Instance.CancelPlan(skill);
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
