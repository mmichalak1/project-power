using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntityDataHolder : MonoBehaviour
{

    [SerializeField]
    private EntityData _sheepData;
    public void LoadSheepData(EntityData data)
    {
        SheepData = data;
        gameObject.name = data.Name;
        HealthController comp = gameObject.GetComponent<HealthController>();
        data.ResetStats();
        ApplyItemsChange();
        comp.MaxHealth = data.TotalHealth;
        comp.HealToFull();
        comp.Defence = data.TotalDefence;


        foreach (var x in SheepData.SheepSkills.Skills)
            if (x != null)
                x.Initialize(gameObject);
    }   
    public void ApplyItemsChange()
    {
        if (SheepData.DefensiveItem != null)
            SheepData.DefensiveItem.ApplyItemChanges(SheepData);
        if (SheepData.OffensiveItem != null)
            SheepData.OffensiveItem.ApplyItemChanges(SheepData);
    }

    public void RevertItemsChange()
    {
        if (SheepData.DefensiveItem != null)
            SheepData.DefensiveItem.ReverseItemChanges(SheepData);
        if (SheepData.OffensiveItem != null)
            SheepData.OffensiveItem.ReverseItemChanges(SheepData);
    }


    public EntityData SheepData
    {
        get { return _sheepData; }
        private set { _sheepData = value; }
    }
}
