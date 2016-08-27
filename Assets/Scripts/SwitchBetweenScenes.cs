using UnityEngine;

public class SwitchBetweenScenes : MonoBehaviour {

	public string Destination;

	public void LoadScene()
	{
        Assets.LogicSystem.Events.Instance.Reset();
		Application.LoadLevel(Destination);
	}
}
