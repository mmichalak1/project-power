using UnityEngine;

[CreateAssetMenu(fileName = "GameSave", menuName = "Game/GameSave")]
public class GameSave : ScriptableObject {

    public EntityData[] SheepData;
    public LevelData[] LevelData;
    public PlayerData PlayerData;
    public ItemsLists AllItems;
    public AllSkills AllSkills;
    public WoolCounter WoolCounter;
    public ResourceCounter ResourceCounter;
    public TutorialsList Tutorials;
}
