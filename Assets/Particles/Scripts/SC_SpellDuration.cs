using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Destroy game object after duration.
 * 
 * @author j@gamemechanix.io
 * @project SpellCraft
 * @copyright GameMechanix.io 2016
 **/
public class SC_SpellDuration : MonoBehaviour {

	[Header("Config")]
	public float spellDuration;
    public List<ParticleSystem> children;

	void Start () {
        foreach(ParticleSystem child in children)
        {
            child.loop = false;
        }
		Destroy (gameObject, spellDuration);
	}
}
