using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public ExplorationHolder holder;
        public LevelData levelData;
        public Image Padlock;
        public Button Backgroung;

        [SerializeField]
        private Text ProgressData;

        // Use this for initialization
        void Start()
        { 
            if (!levelData.IsLocked)
            {
                Padlock.gameObject.SetActive(false);
                Backgroung.interactable = true;
                ProgressData.gameObject.SetActive(true);
            }
            else
            {
                ProgressData.text = levelData.Progress + "/" + levelData.TargetProgress;
                Padlock.gameObject.SetActive(true);
                Backgroung.interactable = false;
                ProgressData.gameObject.SetActive(false);
            }
        }

        private void ProcessResult()
        {
            if (holder.GameResult == GameResult.Win)
                levelData.OnLevelWon();
            else
                levelData.OnLevelLost();
        }
    }
}

