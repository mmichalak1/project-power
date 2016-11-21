using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ChestScript : MonoBehaviour {

    public GameObject Player;
    public UnityEvent OnPlayerEnter;


    private bool isDetecting = true;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player && isDetecting)
        {
            isDetecting = !isDetecting;
            OnPlayerEnter.Invoke();
        }
    }
}
