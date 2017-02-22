using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using UnityEngine.UI;

public class SwipeManager : MonoBehaviour
{
    Vector2 touchStart = -Vector2.one;
    string value;
    int MINDISTANCE = 100;

    Events.MyEvent Enable, Disable;

    // Use this for initialization
    void Start()
    {
        Enable = x => { enabled = true; };
        Disable = x => { enabled = false; };
        Events.Instance.RegisterForMultipleEvents(new[] { "DisableSwipe", "EnterFight" }, Disable);
        Events.Instance.RegisterForEvent("EndFight", Enable);
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
            if (touch.x > touchStart.x && Mathf.Abs(touch.x - touchStart.x) > MINDISTANCE)
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
        }

#endif
#if UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount <= 0) return;
        {
            var touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
                touchStart = touch.position;
            else if (touch.phase == TouchPhase.Ended)
            {
                var deltaX = Mathf.Abs(touch.position.x - touchStart.x);
                var deltaY = Mathf.Abs(touch.position.y - touchStart.y);

                if (deltaX > deltaY)
                {
                    if (deltaX < MINDISTANCE)
                        return;
                    if (touch.position.x < touchStart.x)
                    {
                        Events.Instance.DispatchEvent("TurnLeft", null);
                    }
                    else
                    {
                        Events.Instance.DispatchEvent("TurnRight", null);
                    }
                }
                else
                {
                    if (deltaY < MINDISTANCE)
                        return;
                    if (touch.position.y > touchStart.y)
                    {
                        Events.Instance.DispatchEvent("MoveForward", null);
                    }
                    else
                    {
                        Events.Instance.DispatchEvent("MoveDown", null);
                    }
                }
            }
        }
#endif
    }

}
