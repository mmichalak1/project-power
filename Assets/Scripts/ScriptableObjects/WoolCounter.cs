using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game/WoolCounter")]
public class WoolCounter : ScriptableObject {

    [SerializeField]
    private int _woolCount = 0;

    public int WoolCount
    {
        get { return _woolCount; }
        set
        {
            _woolCount = value;
            GameObject.FindGameObjectWithTag("WoolCounter").GetComponent<WoolUpdater>().UpdateWoolView();
        }
    }
}
