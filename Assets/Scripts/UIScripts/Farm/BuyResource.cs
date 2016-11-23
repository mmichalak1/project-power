using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyResource : MonoBehaviour {

    public WoolCounter woolCounter;
    public Text resources;
    public Text resourcesCost;
    private int cost;
    // Use this for initialization

    void Start()
    {
        updateActualResourcesAndCost();
    }
    public void buyMoreResources()
    {
        if(woolCounter.WoolCount >= cost)
        {
            woolCounter.Resources++;
            woolCounter.WoolCount -= cost;
            updateActualResourcesAndCost();
        }
    }
    void updateActualResourcesAndCost()
    {
        updateCost();
        resources.text = "Resources: "+ woolCounter.Resources.ToString();
        resourcesCost.text = "Buy more for: " + cost;

    }
    void updateCost()
    {
        cost = (woolCounter.Resources - woolCounter.BasicResources + 1) * 5 + 5;
    }
}
