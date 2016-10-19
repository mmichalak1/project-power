using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsLists", menuName = "Game/ItemsLists")]
public class ItemsLists : ScriptableObject {

    public List<Item> WarriorOffensive;
    public List<Item> WarriorDefensive;
    public List<Item> ClericOffensive;
    public List<Item> ClericDefensive;
    public List<Item> MageOffensive;
    public List<Item> MageDefensive;
    public List<Item> RogueOffensive;
    public List<Item> RogueDefensive;

}
