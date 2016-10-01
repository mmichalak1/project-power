using UnityEngine;

public class DamageDebuff : MonoBehaviour, Assets.Scripts.Interfaces.IDisappearAfterTurn
{

    public int Duration = 2;
    [SerializeField, Range(0, 70)]
    private int _debuffValue = 20;


    [HideInInspector]
    public int DebuffValue
    {
        get { return _debuffValue; }
        set
        {
            if (value > 70 || value < 0)
                return;
            _debuffValue = value;
        }
    }



    public void Tick()
    {
        if (--Duration == 0)
            Destroy(this);
    }
}
