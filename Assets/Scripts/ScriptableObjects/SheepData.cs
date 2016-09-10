using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/Sheep")]
public class SheepData : ScriptableObject {

    public string Name = "Wacław";
    public int MaxHealth = 100;
    public int Level = 0;
    public int Experience = 0;
    public int ExperienceToNextLevel = 100;
    public int Attack = 20;
    public int Defence = 0;
    public int Wool = 1;
    public int MaxWool = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
