using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    Vector2 touchStart = -Vector2.one;
    string value;
    const int MINDISTANCE = 75;

    // Use this for initialization
    void Start()
    {
        Events.Instance.RegisterForEvent("EnterFight", x =>
        {
            enabled = false;
        });
        Events.Instance.RegisterForEvent("BattleWon", x =>
        {
            enabled = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var touch = (Vector2)Input.mousePosition;
            if (touch.x > touchStart.x && Mathf.Abs(touch.x-touchStart.x) > MINDISTANCE)
            {
                Events.Instance.DispatchEvent("TurnRight", null);
            }
            else if (touch.x < touchStart.x && Mathf.Abs(touch.x - touchStart.x) > MINDISTANCE)
            {
                Events.Instance.DispatchEvent("TurnLeft", null);
            }
            else if (touch.y > touchStart.y && Mathf.Abs(touch.y - touchStart.y) > MINDISTANCE)
            {
                Events.Instance.DispatchEvent("MoveForward", null);
            }
            else if (touch.y < touchStart.y && Mathf.Abs(touch.y - touchStart.y) > MINDISTANCE)
            {
                Events.Instance.DispatchEvent("MoveDown", null);
            }
            value = "Mouse: X: " + Mathf.Abs(touch.x - touchStart.x) + "Y: " + Mathf.Abs(touch.y - touchStart.y);
        }
        
#endif
#if UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                    touchStart = touch.position;
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (touch.position.x > touchStart.x && Mathf.Abs(touch.position.x - touchStart.x) > MINDISTANCE)
                    {
                        Events.Instance.DispatchEvent("TurnRight", null);
                    }
                    else if (touch.position.x < touchStart.x && Mathf.Abs(touch.position.x - touchStart.x) > MINDISTANCE)
                    {
                        Events.Instance.DispatchEvent("TurnLeft", null);
                    }
                    else if (touch.position.y > touchStart.y && Mathf.Abs(touch.position.y - touchStart.y) > MINDISTANCE)
                    {
                        Events.Instance.DispatchEvent("MoveForward", null);
                    }
                    value = "Touch: X: " + Mathf.Abs(touch.position.x - touchStart.x) + "Y: " + Mathf.Abs(touch.position.y - touchStart.y);
            }
        }
#endif
    }

    void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 100, 100), value);
    }

    
}
