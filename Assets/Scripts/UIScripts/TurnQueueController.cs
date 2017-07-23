using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Assets.LogicSystem;

public class TurnQueueController : MonoBehaviour
{
    public BattleResourcesController resourceController;

    public GameObject SkillPrefab;

    private Dictionary<Plan, GameObject> _plannedSkills = new Dictionary<Plan, GameObject>();
    private PlanComparator planComparator = new PlanComparator();

    public void AddPlan(Plan plan)
    {
        if (_plannedSkills.ContainsKey(plan))
            _plannedSkills.Remove(plan);

        var go = Instantiate(SkillPrefab);
        go.transform.SetParent(gameObject.transform, false);
        go.GetComponent<Image>().sprite = plan.Skill.Icon;
        go.GetComponent<Button>().onClick.AddListener(() => { CancelPlan(plan); });
        _plannedSkills.Add(plan, go);

    }


    public void RemovePlan(Plan plan)
    {
        if (!_plannedSkills.ContainsKey(plan))
            return;
        Destroy(_plannedSkills[plan]);
        _plannedSkills.Remove(plan);
    }

    public void CancelPlan(Plan plan)
    {
        if (!_plannedSkills.Keys.Contains(plan, planComparator))
            return;
        RemovePlan(plan);
        resourceController.TakeResources(-plan.Skill.Cost);
        TurnPlaner.Instance.CancelPlan(plan);
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
