using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorScript : MonoBehaviour
{

    public float IndicationSpeed = 1f;
    public float IndicationTime = 5.0f;
    public Color DealtDamageColor, HealedColor;
    public GameObject IndicatorPrefab;

    public void BeginIndication(int HealthPointChange)
    {
        //create a new item, name it, and set the parent
        GameObject newItem = Instantiate(IndicatorPrefab) as GameObject;
        newItem.name = "item";
        newItem.transform.SetParent(gameObject.transform);
        
        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localPosition = Vector3.zero;
        rectTransform.offsetMin = new Vector2(0,0);

        var text = newItem.GetComponent<Text>();

        //Setup indication direction and speed
        var indicator = newItem.GetComponent<MoveInDirection>();
        indicator.Direction = Vector3.up;
        indicator.LifeTime = IndicationTime;
        indicator.Speed = IndicationSpeed;

        text.text = HealthPointChange.ToString();

        if (HealthPointChange > 0)
        {
            text.color = HealedColor;
        }
        else
        {
            text.color = DealtDamageColor;
        }

        text.enabled = true;
    }


   
}
