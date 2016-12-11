using UnityEngine;
using System.Collections;

public class EntityAffiliation : MonoBehaviour {

    [SerializeField]
    private Affiliation _myAffiliation;

    public Affiliation Affiliation
    {
        get { return _myAffiliation; }
        set { _myAffiliation = value; }
    }
	
}



