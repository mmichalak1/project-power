using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SheepDataHolder : MonoBehaviour
{

    [SerializeField]
    private SheepData _sheepData;
    public void LoadSheepData(SheepData data)
    {
        SheepData = data;
        gameObject.name = data.Name;
        var comp = gameObject.GetComponent<HealthController>();
        comp.MaxHealth = data.MaxHealth;
        comp.HealToFull();
        data.ResetStats();
        ApplyItemsChange();

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


    public SheepData SheepData
    {
        get { return _sheepData; }
        private set { _sheepData = value; }
    }
}
