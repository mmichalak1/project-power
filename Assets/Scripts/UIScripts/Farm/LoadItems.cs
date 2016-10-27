using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LoadItems : MonoBehaviour
{
    public Item.ItemType type = Item.ItemType.Defensive;
    public SheepData[] Sheep;
    public GameObject ItemPrefab;
    public ItemsLists ItemsLists;
    public ItemDataPanel DataPanel;
    public WoolUpdater WoolUpdater;

    private int selectedSheep;
    private List<GameObject> ItemsIcons = new List<GameObject>();


    void Start()
    {
        Load(0);
    }

    public void Load(int sheepNumber)
    {
        selectedSheep = sheepNumber;
        List<Item> Items = null;
        foreach (var item in ItemsIcons)
        {
            Destroy(item);
        }
        Items = ItemsLists.LoadItems(Sheep[sheepNumber].SheepClass, type);
        Items.Sort(new Item.ItemComparer());

        foreach (var item in Items)
        {
            var newItem = Instantiate(ItemPrefab);
            ItemsIcons.Add(newItem);
            newItem.transform.SetParent(gameObject.transform, false);
            newItem.GetComponent<LoadItemData>().LoadData(item, Sheep[sheepNumber]);
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() =>
           {
               DataPanel.gameObject.SetActive(true);
               DataPanel.Load(newItem.GetComponent<LoadItemData>());
           });

        }
    }

    public void Refresh()
    {
        Load(selectedSheep);
    }
}
