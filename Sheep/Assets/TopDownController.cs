using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class TopDownController : MonoBehaviour
{



    private float actionDelay = 0.5f;
    private float actionTimer = 0.0f;
    private GameObject[] grid;
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 180.0f;
    private Vector3 newPos;
    private Quaternion newRot;
    private bool move = false;


    // Use this for initialization
    void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("Tile");
        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Events.Instance.RegisterForEvent("TurnRight", x => RotateRight());
        Events.Instance.RegisterForEvent("TurnLeft", x => RotateLeft());
        Events.Instance.RegisterForEvent("MoveForward", x => MoveForward());
        Events.Instance.RegisterForEvent("MoveDown", x => MoveBack());
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * rotationSpeed);

            actionTimer += Time.deltaTime;
            if (actionDelay < actionTimer)
            {
                actionTimer = 0;
                move = false;
            }
        }
    }

    public void MoveForward()
    {
        if (!move)
        {
            newPos = transform.position + transform.TransformDirection(Vector3.forward).normalized * 2;
            move = true;
        }
    }

    public void RotateLeft()
    {
        if (!move)
        {
            newPos = transform.position + transform.TransformDirection(Vector3.left).normalized * 2;
            move = true;
        }
    }

    public void RotateRight()
    {
        if (!move)
        {
            newPos = transform.position + transform.TransformDirection(Vector3.right).normalized * 2;
            move = true;
        }
    }
    public void MoveBack()
    {
        if (!move)
        {
            newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.back).normalized * 2;
            move = true;
        }
    }
}
