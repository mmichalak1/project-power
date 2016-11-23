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
        comp.MaxHealth = data.MaxHealth;
        comp.HealToFull();
        comp.Defence = calculateDefence(data);


        foreach (var x in SheepData.SheepSkills.Skills)
            if (x != null)
                x.Initialize(gameObject);
    }   
    public double calculateDefence(EntityData data)
    {
        double maxDefenceFromItem = 60;
        double wool = data.Wool;
        double maxWool = data.MaxWool;
        double defence = data.BasicDefence;
        //maximum defence from wool is 30 %, plus warrior can make another 30 % from items
        double defenceFromWool =  (wool / maxWool) * 0.3;
        double defenceFromItems = (defence / maxDefenceFromItem) * 0.3;
        double result = defenceFromWool + defenceFromItems;
        if (result > 60)
            return 60;
        return result;

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
