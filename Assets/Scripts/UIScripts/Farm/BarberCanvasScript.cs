using UnityEngine;
using UnityEngine.UI;

public class BarberCanvasScript : MonoBehaviour
{
    [SerializeField]
    private WoolCounter Counter;
    const int SheepCounter = 4;

    public Slider[] sliders;
    public Text[] Names;
    public EntityData[] sheepData;
    public Text[] SlidersValue;
    public Text[] MaxWoolText;
    public Text[] Defence;
    public Image[] SheepIcons;

    public static int ExpandValue = 10;
    public Text[] TextOfCostOfExpand;
    private int[] CostOfExpand;
    public Text[] TextWoolCap;

    void Start()
    {
        CostOfExpand = new int[4];
        for (int i = 0; i < SheepCounter; i++)
        {
            updateActualCost(i);
            SetSliderAndDefence(i);
            TextWoolCap[i].text = "Wool: " + sheepData[i].Wool + "/" + sheepData[i].MaxWool;
            MaxWoolText[i].text = sheepData[i].Wool.ToString();
            Names[i].text = sheepData[i].Name;
            SheepIcons[i].sprite = sheepData[i].Portrait;
        }
    }
    void SetSliderAndDefence(int sheepNumber)
    {
        sliders[sheepNumber].wholeNumbers = true;
        sliders[sheepNumber].maxValue = sheepData[sheepNumber].Wool;
        sliders[sheepNumber].minValue = 0;
        Defence[sheepNumber].text = "Defence from wool: " + sheepData[sheepNumber].DefenceFromWool().ToString() + " %";
    }
    public void UpdateSliderText(int number)
    {
        SlidersValue[number].text = sliders[number].value.ToString();
        Defence[number].text = "Defence from wool: " + PredictedDefence(number, sliders[number].value) + " %";
    }
    public int PredictedDefence(int sheepNumber, float value)
    {
        return (int)(((float)(sheepData[sheepNumber].Wool - value) / (float)sheepData[sheepNumber].MaxWool) * 30);
    }

    public void ApplyChanges()
    {

        for (int i = 0; i < SheepCounter; i++)
        {
            Counter.WoolCount += (int)sliders[i].value;
            sheepData[i].Wool -= (int)sliders[i].value;
        }
    }
    public void buyMoreResources(int sheepNumber)
    {
        if (Counter.WoolCount >= CostOfExpand[sheepNumber])
        {
            sheepData[sheepNumber].MaxWool += ExpandValue;
            Counter.WoolCount -= CostOfExpand[sheepNumber];
            updateActualCost(sheepNumber);
            UpdateSliderText(sheepNumber);
            TextWoolCap[sheepNumber].text = "Wool: " + sheepData[sheepNumber].Wool + "/" + sheepData[sheepNumber].MaxWool;
        }
    }
    void updateActualCost(int sheepNumber)
    {
        updateCost(sheepNumber);
        TextOfCostOfExpand[sheepNumber].text = "Expand for: " + CostOfExpand[sheepNumber];
    }
    void updateCost(int sheepNumber)
    {
        CostOfExpand[sheepNumber] = sheepData[sheepNumber].MaxWool * 2;
    }

}
