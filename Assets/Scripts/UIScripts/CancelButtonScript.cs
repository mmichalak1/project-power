using UnityEngine;
using System.Collections;

public class CancelButtonScript : MonoBehaviour
{

    public int UpperY = 20;
    public int LowerY = -125;
    public float Speed = 300.0f;

    private RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.state == TurnManager.activeState.skillPicked)
        {
            if (rectTransform.position.y < UpperY)
                rectTransform.position += new Vector3(0, Speed, 0) * Time.deltaTime;
        }
        else
            if (rectTransform.position.y > LowerY)
                rectTransform.position -= new Vector3(0, Speed, 0) * Time.deltaTime;
    }
}
