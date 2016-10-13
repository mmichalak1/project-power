using UnityEngine;
using System.Collections;

using Assets.LogicSystem;

public class MovementController : MonoBehaviour
{
    readonly Vector3 targetDirection = new Vector3(2, 1, 0).normalized;

    private float actionDelay = 0.5f;
    private float actionTimer = 0.0f;
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 180.0f;
    private Vector3 newPos;
    private Quaternion newRot;
    private bool move = false;
    private Ray myRay;

    public Transform forward;

    // Use this for initialization
    void Start()
    {
        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Events.Instance.RegisterForEvent("TurnRight", x => RotateRight());
        Events.Instance.RegisterForEvent("TurnLeft", x => RotateLeft());
        Events.Instance.RegisterForEvent("MoveForward", x => MoveForward());
        Events.Instance.RegisterForEvent("MoveDown", x => MoveBack());
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = new Ray(transform.position + Vector3.up, (forward.position - transform.position).normalized);
        Debug.DrawLine(myRay.origin, myRay.origin + myRay.direction * 3);
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
            RaycastHit res;

            if (Physics.Raycast(myRay.origin, myRay.origin + myRay.direction * 3, out res))
            {
                Debug.Log("Detected " + res.collider.gameObject.name);
                if (res.collider.gameObject.transform.root.tag == "Unpassable")
                {
                    Debug.Log("Tried to walk into " + res.collider.gameObject.name);
                    return;
                }
            }

            newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.right).normalized * 2;
            move = true;
        }
    }

    public void MoveBack()
    {
        if (!move)
        {
            newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.left).normalized * 2;
            move = true;
        }
    }

    public void RotateLeft()
    {
        if (!move)
        {
            newRot = transform.rotation * Quaternion.AngleAxis(-90f, Vector3.up);
            move = true;
        }
    }

    public void RotateRight()
    {
        if (!move)
        {
            newRot = transform.rotation * Quaternion.AngleAxis(90f, Vector3.up);
            move = true;
        }
    }

}
