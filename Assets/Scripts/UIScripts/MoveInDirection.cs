using UnityEngine;
using System.Collections;

public class MoveInDirection : MonoBehaviour {

    public Vector3 Direction;
    public float Speed;
    public float LifeTime = 2f;

    RectTransform myTransform;
    float timer = 0f;

    void Start()
    {
        Direction.Normalize();
        myTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        myTransform.Translate(Direction * Speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= LifeTime)
            Destroy(gameObject);
    }
}
