using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AfterBattleScreen : MonoBehaviour {

    [SerializeField]
    private WoolCounter Counter = null;
    private int woolForFight = 0;
    private int woolToDisplay;
    private int woolGainedDisplay;
    private int condition;
    private int step;
    private float delta = 0;

    public Text WoolGrowIndicator;
    public Text WoolGained;
    public int stepFactor;
    public float delay;
    public float waitTime;
    private State state;
	
	void Update () {

        switch (state)
        {
            case State.Play:
                {
                    Play();
                } break;
            case State.Wait:
                {
                    StartCoroutine(Wait(waitTime));
                }break;
            default:
                break;
        }

	}

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        state = State.Play;
    }

    private void Play()
    {
        delta += Time.deltaTime;
        if (delta >= delay)
        {
            if (woolToDisplay + step < condition)
            {
                woolToDisplay += step;
                woolGainedDisplay -= step;
                WoolGrowIndicator.text = woolToDisplay.ToString();
                WoolGained.text = " + " + woolGainedDisplay.ToString();
            }
            else
            {
                woolToDisplay = condition;
                woolGainedDisplay = 0;
                WoolGrowIndicator.text = woolToDisplay.ToString();
                WoolGained.text = " + " + woolGainedDisplay.ToString();
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

    public void Show(int woolForFight)
    {
        gameObject.SetActive(true);
        WoolGrowIndicator.text = Counter.WoolCount.ToString();
        woolToDisplay = Counter.WoolCount;
        woolGainedDisplay = woolForFight;
        WoolGained.text = " + " + woolGainedDisplay.ToString();
        condition = woolToDisplay + woolForFight;
        step = (int)(woolForFight / stepFactor);
        state = State.Wait;
        if (step == 0)
            step = 1;
    }

    private enum State
    {
        Play,
        Wait
    };
}
