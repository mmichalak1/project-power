using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;

public class SwipeManager : MonoBehaviour, ISystem
{
    Vector2 touchStart = -Vector2.one;
    string value;
    public int minDistanceSwipe = 100;
    public int minDistanceHold = 60;
    public float timeToCheckSwipeHold = 0.3f;
    public bool wasMovementHeld = false;
    Events.MyEvent Enable, Disable;



    private void Awake()
    {
        SystemAccessor.AddSystem(this);
    }
    private void OnDestroy()
    {
        SystemAccessor.RemoveSystem(this);
    }

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
        ParseSwipe();
    }
    private void ParseSwipe()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            Debug.Log("Starting waiting coroutine.");
            StartCoroutine("CheckForHolding");
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(!wasMovementHeld)
            {
                StopCoroutine("CheckForHolding");
                var touch = (Vector2)Input.mousePosition;
                if (touch.x > touchStart.x && Mathf.Abs(touch.x - touchStart.x) > minDistanceSwipe)
                {
                    Events.Instance.DispatchEvent("TurnRight", null);
                }
                else if (touch.x < touchStart.x && Mathf.Abs(touch.x - touchStart.x) > minDistanceSwipe)
                {
                    Events.Instance.DispatchEvent("TurnLeft", null);
                }
                else if (touch.y > touchStart.y && Mathf.Abs(touch.y - touchStart.y) > minDistanceSwipe)
                {
                    Events.Instance.DispatchEvent("MoveForward", null);
                }
                else if (touch.y < touchStart.y && Mathf.Abs(touch.y - touchStart.y) > minDistanceSwipe)
                {
                    Events.Instance.DispatchEvent("MoveDown", null);
                }
            }
            else
            {
                wasMovementHeld = false;
            }
        }

        if(Input.GetMouseButton(0) && wasMovementHeld)
        {
            Debug.Log("Moving");
            var touch = (Vector2)Input.mousePosition;
            if (touch.x > touchStart.x && Mathf.Abs(touch.x - touchStart.x) > minDistanceHold)
            {
                Events.Instance.DispatchEvent("TurnRight", null);
            }
            else if (touch.x < touchStart.x && Mathf.Abs(touch.x - touchStart.x) > minDistanceHold)
            {
                Events.Instance.DispatchEvent("TurnLeft", null);
            }
            else if (touch.y > touchStart.y && Mathf.Abs(touch.y - touchStart.y) > minDistanceHold)
            {
                Events.Instance.DispatchEvent("MoveForward", null);
            }
            else if (touch.y < touchStart.y && Mathf.Abs(touch.y - touchStart.y) > minDistanceHold)
            {
                Events.Instance.DispatchEvent("MoveDown", null);
            }
        }


#elif UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
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


    private IEnumerator CheckForHolding()
    {
        yield return new WaitForSeconds(timeToCheckSwipeHold);
        wasMovementHeld = true;
        Debug.Log("Movment is held");
        yield return null;
    }
}

