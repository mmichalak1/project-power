using UnityEngine;
using System.Collections;

public class SwitchBetweenScenes : MonoBehaviour {

	public string Destination;

	public void LoadScene()
	{
		Application.LoadLevel (Destination);
	}
}
