using UnityEngine;
using Assets.LogicSystem;

public class TurnOnByEvent : MonoBehaviour {

    public string EventName;
    public bool DisableOnStart = false;

	// Use this for initialization
	void Start () {
        Events.Instance.RegisterForEvent(EventName, TurnOn);
        if (DisableOnStart)
            gameObject.SetActive(false);
	}

    private void TurnOn(object obj)
    {
        gameObject.SetActive(true);
    }
}
