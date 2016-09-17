using UnityEngine;
using System.Collections;

public class actionsVisable : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckVisabilit()
	{     
		if(target.activeSelf)
			target.SetActive(false);
		else
			target.SetActive(true);
	}
}
