using Assets.LogicSystem;
using UnityEngine;
using UnityEngine.UI;

public class AfterBattlePanelScript : MonoBehaviour
{

    public Text ExplorationResultText;
    public GameObject ExitButton;

    public Image[] ExperienceIndicators;
    public GameObject[] LevelUpIndicators;
    public SheepData[] sheepData;
    public bool addingExperience = false;

    private bool[] isFinished = new bool[4];


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            ExperienceIndicators[i].fillAmount = (float)(sheepData[i].Experience * 1.0f / sheepData[i].ExperienceForNextLevel * 1.0f);
            isFinished[i] = false;
            ExplorationResultText.text = ExplorationResult.Instance.GameResult.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (addingExperience)
        {
            for (int i = 0; i < 4; i++)
            {
                var data = sheepData[i];
                var indicator = ExperienceIndicators[i];
                if(data.ExperienceGained>0)
                {
                    data.Experience++;
                    data.ExperienceGained--;
                    if (data.Experience == data.ExperienceForNextLevel)
                    {
                        LevelUpIndicators[i].SetActive(true);
                        data.LevelUp();
                    }
                    indicator.fillAmount = data.Experience * 1.0f / data.ExperienceForNextLevel * 1.0f;
                }
                if (data.ExperienceGained == 0)
                    isFinished[i] = true;
            }
            if (isFinished[1] && isFinished[2] && isFinished[3] && isFinished[0])
                ExitButton.SetActive(true);


        }


    }
}
