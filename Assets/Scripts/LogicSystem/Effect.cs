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
        if(actor.tag !="Enemy")
        {
            if (isCast)
                actor.GetComponentInChildren<AnimationControl>().SkillOn(skillAnimationIdentificator);
            if (!isCast)
                actor.GetComponentInChildren<AnimationControl>().SkillOff(skillAnimationIdentificator);
        }
        else
        {
            if (isCast)
                actor.GetComponentInChildren<AnimationControl>().SkillOn(0);
            if (!isCast)
                actor.GetComponentInChildren<AnimationControl>().SkillOff(0);
        }

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

