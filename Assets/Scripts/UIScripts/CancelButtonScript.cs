using UnityEngine;
using System.Collections;

public class CancelButtonScript : MonoBehaviour
{

    public int UpperY = 20;
    public int LowerY = -125;
    public float Speed = 300.0f;

    private RectTransform rectTransform;
    private int direction = 0;
    private bool isHidden = true;
    // Use this for initialization
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void Show()
    {
        if (!isHidden)
            return;
        direction = 1;
    }

    public void Hide()
    {
        if (isHidden)
            return;
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 1)
        {
            if(rectTransform.position.y < UpperY)
                rectTransform.position += new Vector3(0, Speed, 0) * Time.deltaTime;
            if (rectTransform.position.y > UpperY)
            {
                direction = 0;
                isHidden = false;
            }
        }
        else if(direction == -1)
        {
            if (rectTransform.position.y > LowerY)
                rectTransform.position -= new Vector3(0, Speed, 0) * Time.deltaTime;
            if (rectTransform.position.y < LowerY)
            {
                direction = 0;
                isHidden = true;
            }
        }
    }
}
