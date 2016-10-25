﻿using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsLists", menuName = "Game/ItemsLists")]
public class ItemsLists : ScriptableObject {

    public ItemsLists TierLower;

    public List<Item> WarriorOffensive;
    public List<Item> WarriorDefensive;
    public List<Item> ClericOffensive;
    public List<Item> ClericDefensive;
    public List<Item> MageOffensive;
    public List<Item> MageDefensive;
    public List<Item> RogueOffensive;
    public List<Item> RogueDefensive;

    public List<Item> LoadItems(SheepData.Class sheepClass, Item.ItemType type)
    {
        var result = new List<Item>();

        switch (sheepClass)
        {
            case SheepData.Class.Warrior:
                if(type == Item.ItemType.Defensive)
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(WarriorDefensive);
                }
                else
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(WarriorOffensive);
                }
                break;
            case SheepData.Class.Mage:
                if(type == Item.ItemType.Defensive)
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(MageDefensive);
                }
                else
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(MageOffensive);
                }
                break;
            case SheepData.Class.Cleric:
                if (type == Item.ItemType.Defensive)
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(ClericDefensive);
                }
                else
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(ClericOffensive);
                }
                break;
            case SheepData.Class.Rouge:
                if (type == Item.ItemType.Defensive)
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(RogueDefensive);
                }
                else
                {
                    if (TierLower != null)
                        result.AddRange(TierLower.LoadItems(sheepClass, type));
                    result.AddRange(RogueOffensive);
                }
                break;
            default:
                break;
        }


        return result;
    }

}


