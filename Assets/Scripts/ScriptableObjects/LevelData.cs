using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int _progress = 0;
    [SerializeField]
    private int _targetProgress = 100;
    [SerializeField]
    private int _visited = 0;
    [SerializeField]
    private int ProgressPerCompletion = 10;
    [SerializeField]
    private LevelData _nextUnblockableLevel;
    [SerializeField]
    private bool isLocked = true;

    [SerializeField]
    private ChestData[] _chests;

    public int Progress
    {
        get { return _progress; }
        set { _progress = value; }
    }
    public int TargetProgress
    {
        get { return _targetProgress; }
        set { _targetProgress = value; }
    }
    public int Visited
    {
        get { return _visited; }
        set { _visited = value; }
    }
    public LevelData NextUnblockableLevel
    {
        get { return _nextUnblockableLevel; }
    }
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }

    public ChestData[] Chests
    {
        get { return _chests; }
    }

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
        else if (_nextUnblockableLevel != null)
        {
            if (_nextUnblockableLevel.IsLocked)
                _nextUnblockableLevel.isLocked = false;
        }
    }

    public void OnLevelLost()
    {
        Visited++;
    }
}
