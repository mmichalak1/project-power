using UnityEngine;

public class RedirectDamage : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    public GameObject newTarget { get; set; }
    public int RedirectCounter;
    public GameObject ParticleEffect;

    public void Tick()
    {
        RedirectCounter--;
        if (RedirectCounter <= 0)
        {
            ParticleEffect.GetComponent<SC_SpellDuration>().enabled = true;
            Destroy(this);
        }
    }
}
