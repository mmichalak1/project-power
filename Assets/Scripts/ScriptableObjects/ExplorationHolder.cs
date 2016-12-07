using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="ExplorationInstance", menuName ="Game/ExplorationHolder")]
public class ExplorationHolder : ScriptableObject {

    public Assets.Scripts.GameResult GameResult = Assets.Scripts.GameResult.None;
    public LevelData LevelPlayed = null;
    public bool EnableBoss;

    public void Reset()
    {
        GameResult = Assets.Scripts.GameResult.None;
        LevelPlayed = null;
        EnableBoss = false;
    }
}
