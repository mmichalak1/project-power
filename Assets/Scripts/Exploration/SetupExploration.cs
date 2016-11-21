using UnityEngine;

public class SetupExploration : MonoBehaviour {

    public ExplorationHolder Holder;

	public void Setup(LevelData currentLevel)
    {
        Holder.GameResult = Assets.Scripts.GameResult.None;
        Holder.LevelPlayed = currentLevel;
    }
}
