using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public ExplorationHolder holder;
        public LevelData levelData;
        public bool IsLocked;
        public Image Padlock;
        public Button Backgroung;

        [SerializeField]
        private Text ProgressData;

        // Use this for initialization
        void Start()
        {
            //if (holder.GameResult != GameResult.None && !IsLocked)
            //    ProcessResult();

            ProgressData.text = levelData.Progress + "/" + levelData.TargetProgress;
        }

        void OnLevelWasLoaded()
        {
            if (!IsLocked)
            {
                Padlock.gameObject.SetActive(false);
                Backgroung.interactable = true;
                ProgressData.gameObject.SetActive(true);
            }
            else
            {
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

