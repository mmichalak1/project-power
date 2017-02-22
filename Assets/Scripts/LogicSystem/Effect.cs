using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Linq;

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

    public void Apply(GameObject actor, GameObject target, bool isCast, int skillAnimationIdentificator)
    {
        Debug.Log(actor.tag);
        if(actor != null && isCast && actor.tag != "Enemy")
            actor.GetComponentInChildren<AnimationControl>().SkillOn(skillAnimationIdentificator);
        if (actor != null && !isCast && actor.tag != "Enemy")
            actor.GetComponentInChildren<AnimationControl>().SkillOff(skillAnimationIdentificator);
        if (_particleEffect != null)
        {
            GameObject go = Instantiate(_particleEffect, Vector3.zero, Quaternion.identity) as GameObject;

            var effects = go.GetComponents<IEffect>();

            foreach (IEffect effect in effects)
                effect.SetUpAction(actor, target);
        }
        if (_sound != null && actor != null && actor.GetComponent<AudioSource>() != null)
        {
            actor.GetComponent<AudioSource>().PlayOneShot(_sound);
        }
    }

}

