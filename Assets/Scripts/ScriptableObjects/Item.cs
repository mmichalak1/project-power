using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class Item : ScriptableObject {

    public ItemType Type = ItemType.Defensive;
    public Image Icon;

    public Stats[] StatsList;
    public int[] ValueList;

    public void ApplyItemChanges(SheepData data)
    {
        for(int i=0; i<StatsList.Length; i++)
        {
            switch (StatsList[i])
            {
                case Stats.Attack:
                    data.Attack += ValueList[i];
                    break;
                case Stats.Defense:
                    data.Defence += ValueList[i];
                    break;
                case Stats.WoolCounter:
                    data.Wool += ValueList[i];
                    break;
                case Stats.Health:
                    data.MaxHealth += ValueList[i];
                    break;
                default:
                    Debug.LogError("Stat type not found");
                    break;
            }
        }

    }


    public void ReverseItemChanges(SheepData data)
    {
        for (int i = 0; i < StatsList.Length; i++)
        {
            switch (StatsList[i])
            {
                case Stats.Attack:
                    data.Attack -= ValueList[i];
                    break;
                case Stats.Defense:
                    data.Defence -= ValueList[i];
                    break;
                case Stats.WoolCounter:
                    data.Wool -= ValueList[i];
                    break;
                case Stats.Health:
                    data.MaxHealth -= ValueList[i];
                    break;
                default:
                    Debug.LogError("Stat type not found");
                    break;
            }
        }
    }
    







    public enum ItemType
    {
        Offensive,
        Defensive
    }

    public enum Stats
    {
        Attack,
        Defense,
        WoolCounter,
        Health,
    }
}
