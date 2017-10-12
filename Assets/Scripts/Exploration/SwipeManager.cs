using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;

public class SwipeManager : MonoBehaviour, ISystem
{
    Vector2 touchStart = -Vector2.one;
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

        //start listening for swipe hold
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            //Debug.Log("Starting waiting coroutine.");
            StartCoroutine("CheckForHolding");
            return;
        }

        //check what happens when stop holding
        if (Input.GetMouseButtonUp(0))
        {
            //simple swipe
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
                //clean up
                wasMovementHeld = false;
            }
        }

        //it is held, spam event
        if(Input.GetMouseButton(0) && wasMovementHeld)
        {
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
            {
                touchStart = touch.position;
                Debug.Log("Starting waiting coroutine.");
                StartCoroutine("CheckForHolding");
            }       
            else if (touch.phase == TouchPhase.Ended)
            {
                if (!wasMovementHeld)
                {
                    var deltaX = Mathf.Abs(touch.position.x - touchStart.x);
                    var deltaY = Mathf.Abs(touch.position.y - touchStart.y);

                    if (deltaX > deltaY)
                    {
                        if (deltaX < minDistanceSwipe)
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
                        if (deltaY < minDistanceSwipe)
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
                else
                {
                    wasMovementHeld = false;
                }
            }
            else if(touch.phase == TouchPhase.Stationary && wasMovementHeld)
            {
                var deltaX = Mathf.Abs(touch.position.x - touchStart.x);
                var deltaY = Mathf.Abs(touch.position.y - touchStart.y);

                if (deltaX > deltaY)
                {
                    if (deltaX < minDistanceHold)
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
                    if (deltaY < minDistanceHold)
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
        yield return null;
    }

}

