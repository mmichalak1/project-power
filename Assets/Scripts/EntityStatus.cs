using UnityEngine;
public class EntityStatus : MonoBehaviour
{
    [SerializeField]
    private bool stunned = false;
    [SerializeField]
    private bool taunted = false;
    [SerializeField]
    private bool targetable = true;
    [SerializeField]
    private bool alive = true;

    public bool Stunned { get { return stunned; } set { stunned = value; } }
    public bool Poisoned { get { return PoisonEffects > 0; } }
    public bool Taunted { get { return taunted; } set { taunted = value; } }
    public bool Targetable { get { return targetable; } set { targetable = value; } }
    public bool Alive { get { return alive;} set { alive = value; } }

    public int PoisonEffects { get; set; }

}
