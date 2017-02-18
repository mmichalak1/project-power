using UnityEngine;
using System.Collections;
using Assets.LogicSystem;

public class EntityCanvasScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Events.Instance.RegisterForEvent("ShowHealthBar", Show);
        Events.Instance.RegisterForEvent("BattleWon", Hide);
        Events.Instance.RegisterForEvent("BattleLost", Hide);
        gameObject.SetActive(false);
	}

    void Show(object obj)
    {
        gameObject.SetActive(true);
        ClearDamageIndicators();
    }

    void Hide(object obj)
    {
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Events.Instance.UnregisterForEvent("ShowHealthBar", Show);
        Events.Instance.UnregisterForEvent("BattleWon", Hide);
        Events.Instance.UnregisterForEvent("BattleLost", Hide);
    }

    public void ClearDamageIndicators()
    {
        for (int i = transform.childCount - 1; i > 1; i--)
            Destroy(transform.GetChild(i).gameObject);
    }
}
