using UnityEngine;
using System.Collections;

public class SheepController : MonoBehaviour {

     RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
            if (Input.GetMouseButtonDown(0))
            {
                checkTouch(Input.mousePosition);
            }
        }
	}

    void checkTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        var hit = Physics2D.OverlapPoint(touchPos);
        if (hit)
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
