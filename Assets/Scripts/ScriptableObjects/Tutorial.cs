using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Tutorial", menuName ="Game/Tutorial")]
public class Tutorial : ScriptableObject {

    [SerializeField]
    private bool _wasSeen;
    [SerializeField, TextArea(3, 5)]
    private string _text;
   

    public bool WasSeen
    {
        get { return _wasSeen; }
        set { _wasSeen = value; }
    }


    public string Text
    {
        get { return  _text; }
        set { _text = value; }
    }

}
