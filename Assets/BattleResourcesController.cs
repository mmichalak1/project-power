using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class BattleResourcesController : MonoBehaviour {

    public ResourcesDisplay resourcesDisplay;
    public ResourceCounter resourceCounter;


    [SerializeField]
    private int currentResources = 0;

    public int CurrentResources { get { return currentResources; } set { currentResources = value; } }

    private void Start()
    {
        currentResources = resourceCounter.Resources;
    }


    public bool TryAllocateResources(int cost)
    {
        if (currentResources < cost)
            return false;
        Events.Instance.DispatchEvent("SetActive", currentResources);
        return true;

    }

    public void TakeResources(int i)
    {
        currentResources -= i;
        if (currentResources > resourceCounter.Resources)
            currentResources = resourceCounter.Resources;
        Events.Instance.DispatchEvent("SetFilled", currentResources);
    }
}
