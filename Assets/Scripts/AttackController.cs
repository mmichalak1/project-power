using UnityEngine;
using Assets.Scripts.ScriptableObjects;

public class AttackController : MonoBehaviour
{
    private float timer;
    public float WaitTime = 2f;

    [SerializeField]
    public AbstractBrain MyBrain;



    // Use this for initialization
    void Start()
    {
        MyBrain.Initialize(GameObject.FindGameObjectsWithTag("Sheep"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PerformAction()
    {
        if (gameObject.activeSelf)
            MyBrain.Think(gameObject);
    }
}
