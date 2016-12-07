using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyResource : MonoBehaviour {

    public GameObject BuyButton;
    public ResourceCounter resourceCounter;
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
            resourceCounter.Resources++;
            woolCounter.WoolCount -= cost;
            updateActualResourcesAndCost();
        }
    }
    void updateActualResourcesAndCost()
    {
        if(resourceCounter.Resources == resourceCounter.MaxResources)
        {
            BuyButton.SetActive(false);
        }
        updateCost();
        resources.text = "Resources: "+ resourceCounter.Resources.ToString();
        resourcesCost.text = "Buy more for: " + cost;

    }
    void updateCost()
    {
        cost = (resourceCounter.Resources - resourceCounter.BasicResources + 1) * 5 + 5;
    }
}
