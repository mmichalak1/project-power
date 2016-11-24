using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameSave", menuName = "Game/GameSave")]
public class GameSave : ScriptableObject {

    public EntityData[] SheepData;
    public ItemsLists AllItems;
    public SkillsHolder AllSkills;
    public WoolCounter WoolCounter;
    public ResourceCounter ResourceCounter;
}
