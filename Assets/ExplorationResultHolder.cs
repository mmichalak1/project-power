using UnityEngine;
using System.Collections;

public class ExplorationResultHolder : MonoBehaviour {

    public ExplorationHolder holder;

    public Assets.Scripts.GameResult Result;

    public void SetResult()
    {
        holder.GameResult = Result;
    }
}
