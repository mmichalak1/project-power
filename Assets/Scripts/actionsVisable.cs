using UnityEngine;
using System.Collections;

public class actionsVisable : MonoBehaviour {

	public GameObject gameObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckVisabilit()
	{     
		if(gameObject.active)
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
	}
}
