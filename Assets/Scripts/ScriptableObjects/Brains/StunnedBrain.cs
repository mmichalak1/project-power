using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Stun Brain", menuName ="Game/Brains/Stun Brain")]
    public class StunnedBrain : AbstractBrain {
        public override void Initialize(GameObject[] targets)
        {

        }

        public override void Think(GameObject parent)
        {
            Debug.Log(parent.name + " is stunned, turns left: " + (Duration - 1));
            parent.GetComponent<AttackController>().BreakTurn = true;
            base.Think(parent);
        }

        public void SetDuration(int newDuration)
        {
            Duration = newDuration;
        }
    }
}


