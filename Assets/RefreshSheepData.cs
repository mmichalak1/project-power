using UnityEngine;
using System.Collections.Generic;

public class RefreshSheepData : MonoBehaviour {

    public List<EntityData> Sheep;

    public void ReapplyStatistics()
    {
        foreach (var item in Sheep)
        {
            item.ResetStats();
            EntityDataHolder.RevertItemsChange(item);
            EntityDataHolder.ApplyItemsChange(item);
        }
    }
}
