using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.LogicSystem;

public class ResourcesDisplay : MonoBehaviour
{
    public ResourceCounter ResourceCtr;
    public List<GameObject> Crystals;
    private Color active = Color.white;
    private Color inActive = new Color(0.5f, 0.5f, 0.5f, 1);

    // Use this for initialization
    void Start()
    {
        Events.Instance.RegisterForEvent("ShowResourcesDisplay", Show);
        Events.Instance.RegisterForEvent("BattleWon", Hide);
        Events.Instance.RegisterForEvent("BattleLost", Hide);
        gameObject.SetActive(false);
    }
    void Show(object obj)
    {
        gameObject.SetActive(true);
    }

    void Hide(object obj)
    {
        gameObject.SetActive(false);
    }
    
    public void SetMaxResources(int res)
    {
        for (int i = 0; i < Crystals.Count; i++)
        {
            if (i >= res)
                Crystals[i].SetActive(false);
        }
        PaintCrystals(res, 0, 0);
    }

    public void PaintCrystals(int available, int buffered, int taken)
    {
        int index = 0;
        for(index=0; index<available; index++)
        {
            SetGlowOnCrystal(index, false);
            SetCrystalColor(index, active);
        }

        for (int i = 0; i<buffered+1; i++)
        {
            SetGlowOnCrystal(index, true);
            SetCrystalColor(index, active);
            index++;
        }
        index--;
        for (int i = 0; i < taken+1; i++)
        {
            SetGlowOnCrystal(index, false);
            SetCrystalColor(index, inActive);
            index++;
        }
    }

    private void SetGlowOnCrystal(int index, bool state)
    {
        Crystals[index].transform.GetChild(0).gameObject.SetActive(state);
    }
    
    private void SetCrystalColor(int index, Color color)
    {
        Crystals[index].transform.GetChild(1).GetComponent<Image>().color = color;
    }
}
