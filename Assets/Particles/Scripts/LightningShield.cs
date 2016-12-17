using UnityEngine;
using System.Collections;

public class LightningShield : MonoBehaviour {

    [SerializeField]
    private Material _material;
    [SerializeField]
    private float _speed;
    private Vector2 _offset = new Vector2(0,1);

    void Update () {
        _material.mainTextureOffset += _offset * Time.deltaTime * _speed;
	}
}
