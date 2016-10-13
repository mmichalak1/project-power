using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenuScript : MonoBehaviour {

    public SheepData[] SheepData;

    public Text Name, Attack, HP, WoolCapacity, Defence, Wool;

    void Start()
    {
        LoadData(0);
    }

	public void LoadData(int SheepNumber)
    {
        SheepData data = SheepData[SheepNumber];
        Name.text = data.Name;
        Attack.text = data.BasicAttack.ToString();
        HP.text = data.BasicMaxHealth.ToString();
        WoolCapacity.text = data.MaxWool.ToString();
        Defence.text = data.Defence.ToString();
        Wool.text = data.Wool.ToString() + "/" + data.MaxWool.ToString();
    }
}
