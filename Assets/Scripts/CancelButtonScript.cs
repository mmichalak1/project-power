using UnityEngine;
using System.Collections;

public class CancelButtonScript : MonoBehaviour
{

    private RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.isSkillActive)
        {
            if (rectTransform.position.y < 10)
                rectTransform.position += new Vector3(0, 300, 0) * Time.deltaTime;
        }
        else
            if (rectTransform.position.y > -50)
                rectTransform.position -= new Vector3(0, 300, 0) * Time.deltaTime;
    }
}
