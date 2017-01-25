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


    void Start()
    {
        for (int i = 0; i < SheepCounter; i++)
        {
            SetSliderAndDefence(i);
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
        Defence[sheepNumber].text = "Defence from wool: " + (int)sheepData[sheepNumber].DefenceFromWool() + " %";
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
}
