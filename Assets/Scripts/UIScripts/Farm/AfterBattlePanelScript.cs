using Assets.LogicSystem;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattlePanelScript : MonoBehaviour
{

    public Text ExplorationResultText;
    public GameObject ExitButton;
    public float incomeSpeed;
    public GameSaverScript Saver;

    public Image[] StaticExpIndicator;
    public Image[] DynamicExpIndicator;
    public GameObject[] LevelUpIndicators;
    public EntityData[] sheepData;
    public Image[] Avatars;
    public Text[] SheepLevels;
    public bool addingExperience = false;

    private int[] speedFactor = new int[4];
    private bool[] isFinished = new bool[4];


    // Use this for initialization
    void Start()
    {
        Saver.LoadGame();
        for (int i = 0; i < 4; i++)
        {
            StaticExpIndicator[i].fillAmount = DynamicExpIndicator[i].fillAmount = (float)sheepData[i].Experience / (float)sheepData[i].ExperienceForNextLevel;
            isFinished[i] = false;
            ExplorationResultText.text = ExplorationResult.Instance.GameResult.ToString();
            Avatars[i].sprite = sheepData[i].Portrait;
            SheepLevels[i].text = sheepData[i].Level.ToString();
            sheepData[i].GrowWool();
            speedFactor[i] = (int)(sheepData[i].ExperienceForNextLevel / (1 / incomeSpeed));
        }
        Saver.SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (addingExperience)
        {
            for (int i = 0; i < 4; i++)
            {
                var data = sheepData[i];
                var indicator = DynamicExpIndicator[i];

                if (data.ExperienceGained > 0)
                {
                    if (data.ExperienceGained < speedFactor[i])
                    {
                        data.Experience += data.ExperienceGained;
                        data.ExperienceGained = 0;
                    }
                    else
                    {
                        data.Experience += speedFactor[i];
                        data.ExperienceGained -= speedFactor[i];
                    }
                    if (data.Experience >= data.ExperienceForNextLevel)
                    {
                        LevelUpIndicators[i].SetActive(true);
                        data.LevelUp();
                        indicator.fillAmount = 0.0f;
                        StaticExpIndicator[i].fillAmount = 0.0f;
                        SheepLevels[i].text = sheepData[i].Level.ToString();
                        speedFactor[i] = (int)(sheepData[i].ExperienceForNextLevel / (1 / incomeSpeed));
                    }
                    indicator.fillAmount = data.Experience * 1.0f / data.ExperienceForNextLevel * 1.0f;
                }
                if (data.ExperienceGained == 0)
                    isFinished[i] = true;
            }
            if (isFinished[1] && isFinished[2] && isFinished[3] && isFinished[0])
            {
                addingExperience = false;
                ExplorationResult.Reset();
                ExitButton.SetActive(true);
            }


        }


    }
}
