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

        foreach (var x in SheepData.SheepSkills.Skills)
            if (x != null)
                x.Initialize(gameObject);
    }
    public SheepData SheepData
    {
        get { return _sheepData; }
        private set { _sheepData = value; }
    }
}
