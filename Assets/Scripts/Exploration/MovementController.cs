using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Assets.LogicSystem;

public class MovementController : MonoBehaviour
{
    readonly Vector3 targetDirection = new Vector3(2, 1, 0).normalized;

    private float actionDelay = 0.5f;
    private float actionTimer = 0.0f;
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 180.0f;
    private Vector3 newPos;
    private bool move = false;
    private Ray myRay;
    private int x;
    private int z;
    private bool shallNotPassControl = false;

    public Quaternion newRot;
    public Image ShallNotPass;
    public World World;

    // Use this for initialization
    void Start()
    {
        x = World.SpawnPointX;
        z = World.SpawnPointZ;

        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Events.Instance.RegisterForEvent("TurnRight", a => RotateRight());
        Events.Instance.RegisterForEvent("TurnLeft", a => RotateLeft());
        Events.Instance.RegisterForEvent("MoveForward", a => MoveForward());
        Events.Instance.RegisterForEvent("MoveDown", a => MoveBack());
    }

    // Update is called once per frame
    void Update()
    {
        if (shallNotPassControl)
            Appear();
        else
            Fade();

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
            if (CheckIfAccessible(true))
            {
                newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.right).normalized * 2;
                move = true;
            }
            else
            {
                shallNotPassControl = true;
            }
        }
    }

    public void MoveBack()
    {
        if (!move)
        {
            if (CheckIfAccessible(false))
            {
                newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.left).normalized * 2;
                move = true;
            }
            else
            {
                shallNotPassControl = true;
            }
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

    private bool CheckIfAccessible(bool forward)
    {
        int signal = forward ? 1 : -1;

        switch (Round(transform.eulerAngles.y))
        {
            case 0:
                {
                    if (x + signal < World.width)
                        if (World.Paths[z, x + signal])
                        {
                            x += signal;
                            return true;
                        }

                }
                break;
            case 90:
                {
                    if (z - signal >= 0)
                        if (World.Paths[z - signal, x])
                        {
                            z -= signal;
                            return true;
                        }
                }
                break;
            case 180:
                {
                    if (x - signal >= 0)
                        if (World.Paths[z, x - signal])
                        {
                            x -= signal;
                            return true;
                        }
                }
                break;
            case 270:
                {
                    if (z + signal < World.heigth)
                        if (World.Paths[z + signal, x])
                        {
                            z += signal;
                            return true;
                        }
                }
                break;
            default: { return false; }
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

    void Appear()
    {
        if (ShallNotPass.color.a < 1.0)
        {
            ShallNotPass.color += new Color(0, 0, 0, 10 * Time.deltaTime);
            Debug.Log(ShallNotPass.color.a);
        }
        else
        {
            shallNotPassControl = false;
        }
    }

    void Fade()
    {
        if (ShallNotPass.color.a > 0)
        {
            ShallNotPass.color -= new Color(0, 0, 0,  Time.deltaTime);
        }
    }

}
