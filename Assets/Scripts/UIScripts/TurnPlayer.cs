using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.LogicSystem;
using System;

public class TurnPlayer : MonoBehaviour {


    public void PlayTurn(Action OnEndTurn, Action EnemyThinkAction)
    {
        var queue = TurnPlaner.Instance.Queue;
        TurnPlaner.Instance.Reset();
        StartCoroutine(PlayAction(queue, OnEndTurn, EnemyThinkAction));
    }


    IEnumerator PlayAction(Queue<Plan> queue, Action OnEndTurn, Action EnemyTurns)
    {
        if (queue.Count == 0)
        {
            if(EnemyTurns != null)
            {
                EnemyTurns.Invoke();
                var enemyqueue = TurnPlaner.Instance.Queue;
                StartCoroutine(PlayAction(enemyqueue, OnEndTurn, null));
            }
            else
            {
                Debug.Log("Ending Turn");
                OnEndTurn();
                Debug.Log("Turn Finished");
                yield return null;
            }
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

            yield return StartCoroutine(PlayAction(queue, OnEndTurn, EnemyTurns));
        }
    }

}
