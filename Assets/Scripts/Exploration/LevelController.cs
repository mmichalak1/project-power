using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {

        public LevelData levelData;

        [SerializeField]
        private Text ProgressData;

        // Use this for initialization
        void Start()
        {
            if (LogicSystem.ExplorationResult.Instance.GameResult != GameResult.None)
                ProcessResult();

            ProgressData.text = levelData.Progress + "/" + levelData.TargetProgress;
        }


        private void ProcessResult()
        {
            if (LogicSystem.ExplorationResult.Instance.GameResult == GameResult.Win)
                levelData.OnLevelWon();
            else
                levelData.OnLevelLost();        
        }
    }
}

