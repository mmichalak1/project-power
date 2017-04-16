using Assets.LogicSystem;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattlePanelScript : MonoBehaviour
{
    public ExplorationHolder holder;
    public GameObject ExitButton;
    public float incomeSpeed;
    public GameSaverScript Saver;

    public Image StaticExpIndicator;
    public Image DynamicExpIndicator;
    public GameObject LevelUpIndicator;
    public PlayerData playerData;
    public EntityData[] sheepData;
    public bool addingExperience = false;

    private int speedFactor = 3;
    private bool isFinished = false;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            sheepData[i].GrowWool();
        }
        StaticExpIndicator.fillAmount = DynamicExpIndicator.fillAmount = (float)playerData.Experience / (float)playerData.ExperienceForNextLevel;
        isFinished = false;
        speedFactor = (int)(playerData.ExperienceForNextLevel / (1 / incomeSpeed));

        Saver.SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (addingExperience)
        {
            if (playerData.ExperienceGained > 0)
            {
                if (playerData.ExperienceGained < speedFactor)
                {
                    playerData.Experience += playerData.ExperienceGained;
                    playerData.ExperienceGained = 0;
                }
                else
                {
                    playerData.Experience += speedFactor;
                    playerData.ExperienceGained -= speedFactor;
                }
                if (playerData.Experience >= playerData.ExperienceForNextLevel)
                {
                    playerData.LevelUp();
                    foreach (var sheep in sheepData)
                    {
                        sheep.LevelUp();
                    }
                    DynamicExpIndicator.fillAmount = 0.0f;
                    StaticExpIndicator.fillAmount = 0.0f;
                    speedFactor = (int)(playerData.ExperienceForNextLevel / (1 / incomeSpeed));
                }
                DynamicExpIndicator.fillAmount = playerData.Experience * 1.0f / playerData.ExperienceForNextLevel * 1.0f;
            }
            if (playerData.ExperienceGained == 0)
                isFinished = true;
        }
        if (isFinished)
        {
            addingExperience = false;
            holder.Reset();
            ExitButton.SetActive(true);
        }
    }
}
