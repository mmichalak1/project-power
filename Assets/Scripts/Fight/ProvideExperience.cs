using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthController))]
public class ProvideExperience : MonoBehaviour {

    public int Experience = 40;

    public void ProvideExp()
    {
        var gameStatus = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
        gameStatus.PlayerData.ExperienceGained += Experience;
       
    }

}
