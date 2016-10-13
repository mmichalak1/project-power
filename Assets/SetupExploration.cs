using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class SetupExploration : MonoBehaviour {

	public void Setup(LevelData currentLevel)
    {
        ExplorationResult.Instance.GameResult = Assets.Scripts.GameResult.None;
        ExplorationResult.Instance.LevelPlayed = currentLevel;
    }
}
