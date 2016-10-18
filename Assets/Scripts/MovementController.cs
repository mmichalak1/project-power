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
    private int x = 2;
    private int z = 9;

    public World World;
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
            //RaycastHit res;

            //if (Physics.Raycast(myRay.origin, myRay.origin + myRay.direction * 3, out res))
            //{
            //    Debug.Log("Detected " + res.collider.gameObject.name);
            //    if (res.collider.gameObject.transform.root.tag == "Unpassable")
            //    {
            //        Debug.Log("Tried to walk into " + res.collider.gameObject.name);
            //        return;
            //    }
            //}
            if (CheckIfAccessible())
            {
                newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.right).normalized * 2;
                move = true;
            }
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

    private bool CheckIfAccessible()
    {
        switch (Round(transform.eulerAngles.y))
        {
            case 0:
                {
                    if (x + 1 < World.width)
                        if (World.Paths[z, x + 1])
                        {
                            x++;
                            return true;
                        }

                } break;
            case 90:
                {
                    if (z - 1 >= 0)
                        if (World.Paths[z - 1, x])
                        {
                            z--;
                            return true;
                        }
                } break;
            case 180:
                {
                    if (x - 1 >= 0)
                        if (World.Paths[z, x - 1])
                        {
                            x--;
                            return true;
                        }
                } break;
            case 270:
                {
                    if (z + 1 < World.heigth)
                        if (World.Paths[z + 1, x])
                        {
                            z++;
                            return true;
                        }
                } break;
            default: { return false; }
                break;
        }

        return false;
    }


    private int Round(float number)
    {
        if (80 < number && number < 100)
            return 90;
        if (170 < number && number < 190)
            return 180;
        if (260 < number && number < 280)
            return 270;
        else
            return 0;
    }

}
