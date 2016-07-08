using UnityEngine;
using Assets.LogicSystem;
using System.Collections;

public class SendMessages : MonoBehaviour {

    public void TurnLeft()
    {
        Events.Instance.DispatchEvent("TurnLeft", null);
    }

    public void TurnRight()
    {
        Events.Instance.DispatchEvent("TurnRight", null);
    }

    public void GoForward()
    {
        Events.Instance.DispatchEvent("MoveForward", null);
    }

    public void GoDown()
    {
        Events.Instance.DispatchEvent("MoveDown", null);
    }
}
