using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "effect", menuName = "Game/Effect")]
public class Effect : ScriptableObject
{
    [SerializeField]
    private GameObject _particleEffect;
    [SerializeField]
    public AudioClip _sound;
    [SerializeField]
    public float _duration;


    public AudioClip Sound { get { return _sound; } }
    public float Duration { get { return _duration; } }
    private GameObject ParticleEffect { get { return _particleEffect; } }

    public void Apply(GameObject actor, GameObject target)
    {
        if (_particleEffect != null)
        {
            GameObject go = Instantiate(_particleEffect, actor.transform.position, Quaternion.identity) as GameObject;
            go.transform.position += new Vector3(0, 0.75f, 0);
            go.transform.LookAt(target.transform.position + new Vector3(0, 0.25f, 0));
            go.transform.position += go.transform.forward * 0.25f;
            go.GetComponent<IEffect>().SetUpAction(actor, target);
        }
        if(_sound != null && actor != null && actor.GetComponent<AudioSource>() != null)
        {
            actor.GetComponent<AudioSource>().PlayOneShot(_sound);
        }
    }

}

