using UnityEngine;
using System.Collections;
using Assets.LogicSystem;
using System.Collections.Generic;

public class SheepController : MonoBehaviour
{

    RuntimePlatform platform = Application.platform;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonUp(0))
            {
                checkTouch(Input.mousePosition);
            }
        }
    }

    void checkTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject == gameObject)
            {
                Vector3 wp = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                Debug.Log("hit :: " + hit.transform.gameObject.name + "   touchPos : " + touchPos);
                Events.Instance.DispatchEvent(gameObject.name, new KeyValuePair<Vector2, Transform>(touchPos, transform));
            }
        }
    }
}
