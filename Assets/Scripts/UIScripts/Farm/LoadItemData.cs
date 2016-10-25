using UnityEngine;
using UnityEngine.UI;
    
public class LoadItemData : MonoBehaviour {

    public Sprite[] BackgroundImages;

    public Image Icon, Background, IsEquipted, IsBought;
    public Text ItemCost;

    public Item item;
    public SheepData Sheep;


    public void LoadData(Item item, SheepData selectedSheep)
    {
        this.item = item;
        Sheep = selectedSheep;
        Icon.sprite = item.Icon;
        Background.sprite = BackgroundImages[(int)item.rarity];
        ItemCost.text = item.Cost.ToString();
        if(selectedSheep.OffensiveItem == item || selectedSheep.DefensiveItem == item)
        {
            IsEquipted.gameObject.SetActive(true);
            return;
        }
        if(!item.Bought)
        {
            IsBought.gameObject.SetActive(true);
        }
    }
}
