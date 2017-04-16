using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenuScript : MonoBehaviour {

    public EntityData[] SheepData;

    public Text Name, Attack, HP, WoolDefence, ItemsDefence, SumDef;
    public static Color pushed = new Color(0.83F,0.83F,0.83F,1.0F);
    public static Color unpushed = new Color(0.73F, 0.73F, 0.73F, 1.0F);

    void Start()
    {
        LoadData(0);
    }

	public void LoadData(int SheepNumber)
    {
        EntityData data = SheepData[SheepNumber];
        Name.text = data.Name;
        Attack.text = data.BasicAttack.ToString();
        HP.text = data.BasicMaxHealth.ToString();
        WoolDefence.text = (int)(((float)data.Wool/(float)data.MaxWool) * 30) + "/" + 30;
        ItemsDefence.text = data.DefenceFromItems.ToString();
        SumDef.text = data.TotalDefence.ToString() + "%";
    }

}
