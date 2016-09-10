using UnityEngine;
using System.Collections;

public class SheepDataLoader : MonoBehaviour {

    [SerializeField]
    private GameObject[] Children;

	// Use this for initialization
	void Start () {
        var go = GameObject.FindGameObjectWithTag("GameStatus");
        if(go!=null)
        {
            var sheep = go.GetComponent<GameStatus>().Sheep;
            int i = 0;
            foreach(var sh in sheep)
            {
                Children[i].name = sh.Name;
                var comp = Children[i].GetComponent<HealthController>();
                comp.MaxHealth = sh.MaxHealth;
                comp.HealToFull();
                i++;
            }
        }
	}
	
}
