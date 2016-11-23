using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class Item : ScriptableObject {

    public EntityData.Class SheepClass = EntityData.Class.Warrior;
    public ItemType Type = ItemType.Defensive;
    public Sprite Icon;
    public Rariry rarity = Rariry.Common;
    public int Tier = 1;
    public int Cost = 10;
    public bool Bought = false;

    public Stats[] StatsList;
    public int[] ValueList;

    public void ApplyItemChanges(EntityData data)
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


    public void ReverseItemChanges(EntityData data)
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

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < StatsList.Length; i++)
        {
            result += StatsList[i].ToString() + ": " + ValueList[i] + "\n";
        }

        return result;
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

    public enum Rariry
    {
        Common = 0,
        Epic = 2,
        Rare = 1
        
    }

    public class ItemComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            if (x.Tier != y.Tier)
                return x.Tier.CompareTo(y.Tier);

            return (x.rarity.CompareTo(y.rarity));
        }
    }
}
