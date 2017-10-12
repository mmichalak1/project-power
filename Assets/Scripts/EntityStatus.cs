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
    [SerializeField]
    private bool isDamageBuffed = false;
    [SerializeField]
    private bool isDamageDebuffed = false;
    [SerializeField]
    private bool isDefenceBuffed = false;
    [SerializeField]
    private bool isDefenceDebuffed = false;

    public bool Stunned { get { return stunned; } set { stunned = value; } }
    public bool Poisoned { get { return PoisonEffects > 0; } }
    public bool Taunted { get { return taunted; } set { taunted = value; } }
    public bool Targetable { get { return targetable; } set { targetable = value; } }
    public bool Alive { get { return alive;} set { alive = value; } }
    public bool IsDamageBuffed { get { return isDamageBuffed; } set { isDamageBuffed = value; } }
    public bool IsDamageDebuffed { get { return isDamageDebuffed; } set { isDamageDebuffed = value; } }
    public bool IsDefenceBuffed { get { return isDefenceBuffed; } set { isDefenceBuffed = value; } }
    public bool IsDefenceDebuffed { get { return isDefenceDebuffed; } set { isDefenceDebuffed = value; } }


    public int PoisonEffects { get { return gameObject.GetComponents<PoisonEffect>().Length; } }

}
