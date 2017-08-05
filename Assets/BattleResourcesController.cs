using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using Assets.Scripts.Interfaces;

public class BattleResourcesController : MonoBehaviour, ISystem {

    public ResourcesDisplay resourcesDisplay;
    public ResourceCounter resourceCounter;


    [SerializeField]
    private int maxResources = 0;
    [SerializeField]
    private int availableResources = 0;
    [SerializeField]
    private int bufferedResources = 0;
    [SerializeField]
    private int takenResources = 0;

    public int MaxResources { get { return maxResources; } set { maxResources = value; } }
    #region Unity Hooks
    private void Start()
    {
        MaxResources = resourceCounter.Resources;
        availableResources = MaxResources;
        resourcesDisplay.SetMaxResources(MaxResources);
    }
    private void Awake()
    {
        SystemAccessor.AddSystem(this);
    }

    private void OnDestroy()
    {
        SystemAccessor.RemoveSystem(this);
    }
    #endregion





    public bool MoveToBuffer(int cost)
    {
        if (maxResources < cost)
            return false;
        if(bufferedResources != 0)
            availableResources += bufferedResources;

        bufferedResources = cost;
        availableResources -= cost;
        resourcesDisplay.PaintCrystals(availableResources, bufferedResources, takenResources);
        return true;

    }

    public void MoveFromBufferToTaken()
    {
        takenResources += bufferedResources;
        bufferedResources = 0;
        resourcesDisplay.PaintCrystals(availableResources, bufferedResources, takenResources);

    }

    public void MoveFromTakenToAvailable(int amount)
    {
        if (availableResources + amount > maxResources)
            return;

        if (takenResources - amount < 0)
            return;
        availableResources += amount;
        takenResources -= amount;
        resourcesDisplay.PaintCrystals(availableResources, bufferedResources, takenResources);
    }

    public void ResetState()
    {
        availableResources = MaxResources;
        bufferedResources = 0;
        takenResources = 0;
        resourcesDisplay.PaintCrystals(availableResources, bufferedResources, takenResources);
    }

    public bool FullResources { get { return availableResources == maxResources; } }
}
