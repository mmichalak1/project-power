using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityDataHolder))]
public class SheepWoolDisplay : MonoBehaviour {

    public GameObject[] WoolModels;
    [Range(0, 1)]
    public float[] Boudaries;

    public void SetupWoolModels()
    {
        var holder = gameObject.GetComponent<EntityDataHolder>();
        float currentWool = holder.SheepData.Wool;
        float maxWool = holder.SheepData.MaxWool;
        float woolPercentage = currentWool / maxWool;
        foreach (var item in WoolModels)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < WoolModels.Length; i++)
        {
            if(woolPercentage > Boudaries[i])
            {
                WoolModels[i].SetActive(true);
                return;
            }
        }
    }
}
