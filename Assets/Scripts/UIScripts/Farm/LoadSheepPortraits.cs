using UnityEngine;
using System.Collections;

public class LoadSheepPortraits : MonoBehaviour {

    public EntityData[] Sheep;
    public UnityEngine.UI.Image[] Portraits;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Sheep.Length; i++)
        {
            Portraits[i].sprite = Sheep[i].Portrait;
        }
	}
	
}
