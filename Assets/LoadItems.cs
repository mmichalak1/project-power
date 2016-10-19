using UnityEngine;
using System.Collections.Generic;

public class LoadItems : MonoBehaviour
{
    public Item.ItemType type = Item.ItemType.Defensive;
    public SheepData[] Sheep;
    public GameObject ItemPrefab;
    public ItemsLists ItemsLists;


    private List<GameObject> ItemsIcons = new List<GameObject>();

    void Start()
    {
        Load(0);
    }

    public void Load(int sheepNumber)
    {
        List<Item> Items = null;
        foreach (var item in ItemsIcons)
        {
            Destroy(item);
        }
        switch (Sheep[sheepNumber].SheepClass)
        {
            case SheepData.Class.Warrior:
                if (type == Item.ItemType.Offensive)
                    Items = ItemsLists.WarriorOffensive;
                else
                    Items = ItemsLists.WarriorDefensive;
                break;
            case SheepData.Class.Mage:
                if (type == Item.ItemType.Offensive)
                    Items = ItemsLists.MageOffensive;
                else
                    Items = ItemsLists.MageDefensive;
                break;
            case SheepData.Class.Cleric:
                if (type == Item.ItemType.Offensive)
                    Items = ItemsLists.ClericOffensive;
                else
                    Items = ItemsLists.ClericDefensive;
                break;
            case SheepData.Class.Rouge:
                if (type == Item.ItemType.Offensive)
                    Items = ItemsLists.RogueOffensive;
                else
                    Items = ItemsLists.RogueDefensive;
                break;
            default:
                break;
        }

        foreach (var item in Items)
        {
            var newItem = Instantiate(ItemPrefab);
            ItemsIcons.Add(newItem);
            newItem.transform.SetParent(gameObject.transform, false);
            newItem.GetComponent<LoadItemData>().LoadData(item, Sheep[sheepNumber]);

        }
    }
}
