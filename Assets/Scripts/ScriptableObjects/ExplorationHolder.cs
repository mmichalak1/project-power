using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="ExplorationInstance", menuName ="Game/ExplorationHolder")]
public class ExplorationHolder : ScriptableObject {

    public GameResult GameResult = GameResult.None;
    public LevelData LevelPlayed = null;
    public bool EnableBoss;

    public void Reset()
    {
        GameResult = GameResult.None;
        LevelPlayed = null;
        EnableBoss = false;
    }
}
