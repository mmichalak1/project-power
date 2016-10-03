using UnityEngine;

public class RedirectDamage : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    public GameObject newTarget { get; set; }
    public int RedirectCounter;

    public void Tick()
    {
        RedirectCounter--;
        if (RedirectCounter <= 0)
            Destroy(this);
    }
}
