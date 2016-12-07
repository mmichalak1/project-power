using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "ItemsLists", menuName = "Game/ItemsLists")]
public class ItemsLists : ScriptableObject {

    public List<Item> Items;

    public List<Item> LoadItems(EntityData.Class sheepClass, Item.ItemType type)
    {
        List<Item> result;
        result = Items.Where(x => x.SheepClass == sheepClass && x.Type == type).ToList();
        return result;
    }

}


