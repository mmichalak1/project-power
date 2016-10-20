using UnityEngine;
using UnityEngine.UI;

public class LoadItemData : MonoBehaviour {

    public Sprite[] BackgroundImages;

    public Image Icon, Background, IsEquipted, notBought;
    public Text ItemCost;


    public void LoadData(Item item, SheepData selectedSheep)
    {
        Icon.sprite = item.Icon;
        Background.sprite = BackgroundImages[(int)item.Rarity];
        ItemCost.text = item.Cost.ToString();
        if(selectedSheep.OffensiveItem == item || selectedSheep.DefensiveItem == item)
        {
            IsEquipted.gameObject.SetActive(true);
            return;
        }
        if(!item.Bought)
        {
            notBought.gameObject.SetActive(true);
        }
    }
}
