using UnityEngine;

public class SwitchBetweenScenes : MonoBehaviour {

	public string Destination;

	public void LoadScene()
	{
        Assets.LogicSystem.Events.Instance.Reset();
		UnityEngine.SceneManagement.SceneManager.LoadScene(Destination);
	}
}
