using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenuScript : MonoBehaviour {

    public SheepData[] SheepData;

    public Text Name, Attack, HP, WoolCapacity, Defence, Defence_Bonus_Counter, Wool, Wool_Cap, Exp;
    public Image[] SheepIcon;
    public Image ExpBar;
    public static Color pushed = new Color(0.83F,0.83F,0.83F,1.0F);
    public static Color unpushed = new Color(0.73F, 0.73F, 0.73F, 1.0F);

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
        Defence.text = data.BasicDefence.ToString();
        Defence_Bonus_Counter.text = "+ " + data.Wool.ToString();
        Wool.text = data.Wool.ToString();
        Wool_Cap.text = "/" + data.MaxWool.ToString();
        Exp.text = "Exp " + data.Experience.ToString() + "/" + data.ExperienceForNextLevel.ToString();
        ExpBar.fillAmount = (float)data.Experience / (float)data.ExperienceForNextLevel;
    }

}
