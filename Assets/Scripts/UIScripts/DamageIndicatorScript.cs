using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorScript : MonoBehaviour
{

    public float IndicationSpeed = 1f;
    public float IndicationTime = 5.0f;
    public Color DealtDamageColor, HealedColor;
    public GameObject IndicatorPrefab;

    Vector3 position;
    RectTransform rectTransform;
    float timer = 0f;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        position = rectTransform.position;
    }

    public void BeginIndication(int HealthPointChange)
    {
        var go = Instantiate(IndicatorPrefab);
        var Text = go.GetComponent<Text>();
        go.transform.SetParent(gameObject.GetComponentInChildren<RectTransform>(), false);
        go.GetComponent<RectTransform>().position= new Vector3(0, 10, 0);

        //Setup indication direction and speed
        var indicator = go.GetComponent<MoveInDirection>();
        indicator.Direction = Vector3.up;
        indicator.LifeTime = IndicationTime;
        indicator.Speed = IndicationSpeed;
        if (HealthPointChange > 0)
        {
            Text.color = HealedColor;
        }
        else
        {
            Text.color = DealtDamageColor;
        }
        Text.enabled = true;
    }


   
}
