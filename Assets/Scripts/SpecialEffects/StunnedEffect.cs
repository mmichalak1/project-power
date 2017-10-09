using UnityEngine;
using Assets.Scripts.Interfaces;

public class StunnedEffect : MonoBehaviour, IDisappearAfterTurn {

    [SerializeField]
    private int duration = 2;
	public int Duration { get { return duration; } set { duration = value; } }
    public GameObject ParticleEffect { get; set; }

    public void Tick()
    {
        if (--Duration < 1)
        {
            gameObject.GetComponent<EntityStatus>().Stunned = true;
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Destroy(ParticleEffect);
    }
}
