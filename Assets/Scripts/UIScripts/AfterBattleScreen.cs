using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AfterBattleScreen : MonoBehaviour {

    [SerializeField]
    private WoolCounter Counter;
    private int woolForFight;
    private int woolToDisplay;
    private int condition;
    private int step;
    private float delta = 0;

    public Text WoolGrowIndicator;
    public int stepFactor;
    public float delay;

	void Start () {
        Assets.LogicSystem.Events.Instance.RegisterForEvent("AfterBattleScreen", OnEvoke);
        gameObject.SetActive(false);
    }
	
	void Update () {

        delta += Time.deltaTime;
        if (delta >= delay )
        {
            if (woolToDisplay + step < condition)
            {
                woolToDisplay += step;
                WoolGrowIndicator.text = woolToDisplay.ToString();
            }
            else
            {
                woolToDisplay = condition;
                WoolGrowIndicator.text = woolToDisplay.ToString();
            }
            delta = 0;
        }

	}

    public void ContinueButton()
    {
        gameObject.SetActive(false);
        Counter.WoolCount += woolForFight;
        Assets.LogicSystem.Events.Instance.DispatchEvent("EndFight", null);
    }

    public void OnEvoke(object obj)
    {
        gameObject.SetActive(true);
        WoolGrowIndicator.text = Counter.WoolCount.ToString();
        woolForFight = (int)obj;
        woolToDisplay = Counter.WoolCount;
        condition = woolToDisplay + woolForFight;
        step = (int)(woolForFight / stepFactor);
        if (step == 0)
            step = 1;
    }
}
