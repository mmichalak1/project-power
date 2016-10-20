using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class HPBar : MonoBehaviour {

	void Start () {
        Events.Instance.RegisterForEvent("BattleLost", AnnihilateMyself);
        Events.Instance.RegisterForEvent("BattleWon", AnnihilateMyself);
	}

    void AnnihilateMyself(object obj)
    {
        Destroy(gameObject);
    }
}
