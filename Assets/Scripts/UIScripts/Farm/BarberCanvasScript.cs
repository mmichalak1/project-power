using UnityEngine;
using UnityEngine.UI;

public class BarberCanvasScript : MonoBehaviour
{
    private WoolCounter Counter;
    const int SheepCounter = 4;

    public Slider[] sliders;
    public Text[] Names;
    public EntityData[] sheepData;
    public Text[] SlidersValue;
    public Text[] MaxWoolText;


    void Start()
    {
        for (int i = 0; i < SheepCounter; i++)
        {
            sliders[i].wholeNumbers = true;
            sliders[i].maxValue = sheepData[i].Wool;
            sliders[i].minValue = 0;
            Names[i].text = sheepData[i].Name + " (" + sheepData[i].Wool + ")";
            MaxWoolText[i].text = sheepData[i].Wool.ToString();

        }
    }

    public void UpdateSliderText(int number)
    {
        SlidersValue[number].text = sliders[number].value.ToString();
        Names[number].text = sheepData[number].Name + " (" + (sheepData[number].Wool - sliders[number].value) + ")";
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
