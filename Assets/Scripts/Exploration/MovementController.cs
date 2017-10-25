using UnityEngine;
using System;
using UnityEngine.UI;

using Assets.LogicSystem;

public class MovementController : MonoBehaviour
{
    public TileData currentTile;

    [SerializeField]
    private float rotationDelay = 0.5f;
    [SerializeField]
    private float movementDelay = 0.43f;

    private float actionDelay = 0.0f;

    private float actionTimer = 0.0f;
    private float movementSpeed = 5.0f;
    private float rotationSpeed = 180.0f;
    private Vector3 newPos;
    public bool move = false;
    private bool shallNotPassControl = false;

    public Image ShallNotPass;
    public Quaternion newRot;
    public AnimationControl[] animations;
    public Facing currentFacing;

    // Use this for initialization
    void Start()
    {

        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Events.Instance.RegisterForEvent("TurnRight", RotateRight);
        Events.Instance.RegisterForEvent("TurnLeft", RotateLeft);
        Events.Instance.RegisterForEvent("MoveForward", MoveForward);
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
            foreach (AnimationControl anim in animations)
            {
                anim.Move();
            }
              
            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * rotationSpeed);

            actionTimer += Time.deltaTime;

            if (actionDelay < actionTimer)
            {
                foreach (AnimationControl anim in animations)
                    anim.Stop();
                actionTimer = 0;
                actionDelay = 0.0f;
                move = false;
            }
        }
    }

    public void MoveForward(object x)
    {
        if (!move)
        {
            if (CheckIfAccessible())
            {
                
                newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.TransformDirection(Vector3.right).normalized * 2;
                move = true;
                actionDelay = movementDelay;
            }
            else
            {
                shallNotPassControl = true;
            }
        }
    }

    public void MoveBack(object x)
    {
        if (!move)
        {
            if (CheckIfAccessible())
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

    public void RotateLeft(object x)
    {
        if (!move)
        {
            newRot = transform.rotation * Quaternion.AngleAxis(-90f, Vector3.up);
            move = true;
            currentFacing = getRotationFacing(currentFacing, -1);
            actionDelay = rotationDelay;
        }
    }

    public void RotateRight(object x)
    {
        if (!move)
        {
            newRot = transform.rotation * Quaternion.AngleAxis(90f, Vector3.up);
            move = true;
            currentFacing = getRotationFacing(currentFacing, 1);
            actionDelay = rotationDelay;
        }
    }

    private bool CheckIfAccessible()
    {
        var nextTile = currentTile.GetTile(currentFacing);
        if (nextTile != null && nextTile.ParentBlock.PassableTiles.Contains(nextTile.gameObject))
        {
            currentTile = nextTile;
            return true;
        }
        else if(nextTile==null && currentTile.ParentBlock.ConnectingTiles.Contains(currentTile.gameObject))
        {
            var currBlock = currentTile.ParentBlock;
            GameObject clostestTile = null;
            float distance = 4.0f;

            foreach(var block in currBlock.NeighbouringBlocks)
            {
                foreach(GameObject t in block.ConnectingTiles)
                {
                    float testDistance = Vector3.Distance(currentTile.transform.position, t.transform.position);
                    if(testDistance < distance)
                    {
                        clostestTile = t;
                        distance = testDistance;
                    }
                }
            }
            if (clostestTile == null)
                return false;
            currentTile = clostestTile.GetComponent<TileData>();
            return true;
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
            ShallNotPass.color -= new Color(0, 0, 0, Time.deltaTime);
        }
    }

    //rotation == 1 - right, -1 - left 
    Facing getRotationFacing(Facing currentFace, int rotation)
    {
        if (rotation != 1 && rotation != -1)
            Debug.LogError("Incorrect value");

        int curr = (int)currentFace;

        curr += rotation;

        if (curr < 0)
            return Facing.Left;

        if (curr > 3)
            return Facing.Up;

        return (Facing)curr;
    }
}
