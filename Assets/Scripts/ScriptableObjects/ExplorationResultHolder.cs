using UnityEngine;
using System.Collections;

public class ExplorationResultHolder : MonoBehaviour
{

    public ExplorationHolder holder;

    public Assets.Scripts.GameResult Result;

    public void SetResult()
    {
        if (holder.GameResult != Assets.Scripts.GameResult.Win)
            holder.GameResult = Result;
    }
}
