using UnityEngine;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;

public class AttackController : MonoBehaviour
{
    private float timer;
    public float WaitTime = 2f;
    [SerializeField]
    private AbstractBrain MyBrain;

    [SerializeField]
    private List<AbstractBrain> _brainsList = new List<AbstractBrain>();
    public bool BreakTurn { get; set; }

    // Use this for initialization
    void Start()
    {
        MyBrain.Initialize(GameObject.FindGameObjectsWithTag("Sheep"));
        _brainsList.Add(MyBrain);
    }

    public void PerformAction()
    {
        _brainsList = _brainsList.OrderByDescending(x => x.Importance).ToList();
        if (gameObject.activeSelf)
        {
            foreach (var brain in _brainsList.ToArray())
            {
                brain.Think(gameObject);
                if (BreakTurn)
                {
                    BreakTurn = false;
                    break;
                }
            }
            ClearFinishedBrains();
        }
            

                
    }

    private void ClearFinishedBrains()
    {
        _brainsList.RemoveAll(x => x.Duration == 0);
    }

    public void AddBrain(AbstractBrain brain)
    {
        _brainsList.Add(brain);
    }

    public void RemoveBrain(AbstractBrain brain)
    {
        _brainsList.Remove(brain);
    }

}