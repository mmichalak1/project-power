using UnityEngine;
using System.Collections;
using System;

public class EntitySelector : MonoBehaviour
{

    private Action<GameObject> onTargetSelected { get; set; }
    private Func<GameObject, bool> criteria { get; set; }

    public void StartSearching(Action<GameObject> onTargetSelected, Func<GameObject, bool> criteria)
    {
        this.onTargetSelected = onTargetSelected;
        this.criteria = criteria;
        enabled = true;
    }

    public void StopSearching()
    {
        onTargetSelected = null;
        criteria = null;
        enabled = false;
    }

    private void Update()
    {
        var obj = GetPointedObject();
        if (null != obj)
        {
            onTargetSelected.Invoke(obj);
        }
    }


    GameObject GetPointedObject()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonUp(0))
        {
            return CheckTouch(Input.mousePosition);
        }

#elif UNITY_WSA_10_0 || UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                return CheckTouch(Input.GetTouch(0).position);
               
            }
        }
#endif
        return null;
    }


    private GameObject CheckTouch(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        GameObject hitTarget = null;
        if (Physics.Raycast(ray, out hit))
        {
            if (criteria(hit.collider.gameObject))
                hitTarget = hit.collider.gameObject;
        }
        return hitTarget;
    }
}
