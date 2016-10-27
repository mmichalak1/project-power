using UnityEngine;
using UnityEngine.UI;

public class ItemDataPanel : MonoBehaviour {

    public WoolCounter WoolCounter;

    public Image Icon, Background;

    public Text ItemData, ItemCost;

    public GameObject BuyButton, EquipButton;

    LoadItemData data;

    public void Load(LoadItemData data)
    {
        this.data = data;
        Icon.sprite = data.item.Icon;
        Background.sprite = data.Background.sprite;
        this.ItemCost.text = data.item.Cost.ToString();
        ItemData.text = data.item.ToString();
        if (data.item.Bought)
            BuyButton.SetActive(false);
        else
            BuyButton.SetActive(true);
        if (data.Sheep.OffensiveItem == data.item || data.Sheep.DefensiveItem == data.item)
            EquipButton.SetActive(false);
        else
            EquipButton.SetActive(true);

    }

    public void Buy()
    {
        if(WoolCounter.WoolCount>=data.item.Cost)
        {
            WoolCounter.WoolCount -= data.item.Cost;
            data.item.Bought = true;
            BuyButton.SetActive(false);
            GameObject.FindGameObjectWithTag("WoolCounter").GetComponent<WoolUpdater>().UpdateWoolView(); 
        }
    }

    public void Equip()
    {
        if(data.item.Bought)
        {
            switch (data.item.Type)
            {
                case Item.ItemType.Offensive:
                    data.Sheep.OffensiveItem = data.item;
                    break;
                case Item.ItemType.Defensive:
                    data.Sheep.DefensiveItem = data.item;
                    break;
                default:
                    break;
            }
            EquipButton.SetActive(false);
        }
    }

}
