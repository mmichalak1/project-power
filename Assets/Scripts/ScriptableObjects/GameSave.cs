using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameSave", menuName = "Game/GameSave")]
public class GameSave : ScriptableObject {

    public EntityData[] SheepData;
    public LevelData[] LevelData;
    public ItemsLists AllItems;
    public SkillsHolder AllSkills;
    public WoolCounter WoolCounter;
    public ResourceCounter ResourceCounter;
}
