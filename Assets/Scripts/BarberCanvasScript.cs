﻿using UnityEngine;
using UnityEngine.UI;

public class BarberCanvasScript : MonoBehaviour
{

    const int SheepCounter = 4;

    public Slider[] sliders;
    public Text[] Names;
    public SheepData[] sheepData;
    public Text[] SlidersValue;
    public Text[] MaxWoolText;


    void Start()
    {
        for (int i = 0; i < SheepCounter; i++)
        {
            sliders[i].wholeNumbers = true;
            sliders[i].maxValue = sheepData[i].Wool;
            sliders[i].minValue = 0;
            sliders[i].value = sheepData[i].Wool;
            Names[i].text = sheepData[i].Name;
            MaxWoolText[i].text = sheepData[i].Wool.ToString();
            
        }
    }

    public void UpdateSliderText(int number)
    {
        SlidersValue[number].text = sliders[number].value.ToString();
    }

    public void ApplyChanges()
    {
        var go = GameObject.FindGameObjectWithTag("GameStatus");
        if(go!=null)
        {
            var counter = go.GetComponent<GameStatus>();
            for (int i = 0; i < SheepCounter; i++)
            {
                if(sliders[i].value<sliders[i].maxValue)
                {
                    counter.WoolCounter.WoolCount += System.Convert.ToInt32(sliders[i].maxValue) - System.Convert.ToInt32(sliders[i].value);
                    sheepData[i].Wool = System.Convert.ToInt32(sliders[i].value);
                }
            }
        }
        else
        {
            Debug.LogError("No WoolCounter script found.");
        }
    }


}