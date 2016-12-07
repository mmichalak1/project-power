using UnityEngine;


public class Untargetable : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn {

    public int Duration = 2;

    public void Tick()
    {
        if (--Duration == 0)
            Destroy(this);
    }
}
