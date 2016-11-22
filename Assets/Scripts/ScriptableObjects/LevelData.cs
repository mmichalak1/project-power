using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int _progress = 0;

    public string Name = "Name";
    public int Progress
    {
        get { return _progress; }
        set { _progress = value; }
    }
    public int TargetProgress = 100;
    public int Visited = 0;
    [SerializeField]
    private int ProgressPerCompletion = 10;

    public void OnLevelWon()
    {
        Visited++;
        if (Progress < TargetProgress)
        {
            if (Progress + ProgressPerCompletion > TargetProgress)
                Progress = TargetProgress;
            else
                Progress += ProgressPerCompletion;

        }
    }

    public void OnLevelLost()
    {
        Visited++;
    }

    public void LoadData()
    {

    }
}
