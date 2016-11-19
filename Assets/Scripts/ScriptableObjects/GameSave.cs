using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "GameSave", menuName = "Game/GameSave")]
public class GameSave : ScriptableObject {

    public EntityData[] SheepData;
    public LevelData[] LevelData;
}
