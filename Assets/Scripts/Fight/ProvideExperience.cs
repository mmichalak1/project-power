using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthController))]
public class ProvideExperience : MonoBehaviour {

    const int PartialExperience = 40;

    public int Experience = 40;

    public void ProvideExp()
    {
        var hp = gameObject.GetComponent<HealthController>();
        var sheep = GameObject.FindGameObjectsWithTag("Sheep");
        foreach (var item in sheep)
        {
            if(item == hp.LastAttacker)
                item.GetComponent<EntityDataHolder>().SheepData.ExperienceGained += Experience;
            else
                item.GetComponent<EntityDataHolder>().SheepData.ExperienceGained += (Experience * PartialExperience)/100;
        }
    }

}
