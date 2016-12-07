using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/WoolCounter")]
public class WoolCounter : ScriptableObject
{

    [SerializeField]
    private int _woolCount = 0;

    public int WoolCount
    {
        get { return _woolCount; }
        set
        {
            _woolCount = value;
            if (GameObject.FindGameObjectWithTag("WoolCounter") != null)
            {
                GameObject.FindGameObjectWithTag("WoolCounter").GetComponent<WoolUpdater>().UpdateWoolView();
            }
        }
    }
}
