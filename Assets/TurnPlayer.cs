using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.LogicSystem;
using System;

public class TurnPlayer : MonoBehaviour {


    public void PlayTurn(Action OnEndTurn)
    {
        StartCoroutine(PlayAction(TurnPlaner.Instance.Queue, OnEndTurn));
    }


    IEnumerator PlayAction(Queue<Plan> queue, Action OnEndTurn)
    {
        if (queue.Count == 0)
        {
            Debug.Log("Ending Turn");
            OnEndTurn();
            Debug.Log("Turn Finished");
            yield return null;
        }
        else
        {
            Plan nextPlan = queue.Dequeue();

            if (nextPlan.Skill.OnCastEffect != null)
            {
                nextPlan.Skill.OnCastEffect.Apply(nextPlan.Actor, nextPlan.Target);
                yield return new WaitForSeconds(nextPlan.Skill.OnCastEffect.Duration);
            }

            if (nextPlan.Skill.OnHitEffect != null)
            {
                nextPlan.Skill.OnHitEffect.Apply(nextPlan.Actor, nextPlan.Target);
                yield return new WaitForSeconds(nextPlan.Skill.OnHitEffect.Duration);
            }

            nextPlan.Execute();

            yield return StartCoroutine(PlayAction(queue, OnEndTurn));
        }
    }

}
