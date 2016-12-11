using UnityEngine;
using System.Collections;

public class ExplorationResultHolder : MonoBehaviour
{

    public ExplorationHolder holder;

    public GameResult Result;

    public void SetResult()
    {
        if (holder.GameResult != GameResult.Win)
            holder.GameResult = Result;
    }
}
