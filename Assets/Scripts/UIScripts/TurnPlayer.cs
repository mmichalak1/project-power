using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.LogicSystem;
using System;

public class TurnPlayer : MonoBehaviour
{


    public void PlayTurn(Action OnEndTurn, Action EnemyThinkAction)
    {
        var queue = TurnPlaner.Instance.Queue;
        TurnPlaner.Instance.Reset();
        StartCoroutine(PlayQueue(queue, EnemyThinkAction, OnEndTurn));
    }


    IEnumerator PlayQueue(Queue<Plan> queue, Action OnQueueEnd, Action OnTurnEnd)
    {
        if (queue.Count == 0)
        {
            if(OnQueueEnd != null)
            {
                OnQueueEnd.Invoke();
                var newqueue = TurnPlaner.Instance.Queue;
                StartCoroutine(PlayQueue(newqueue, null, OnTurnEnd));
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                OnTurnEnd.Invoke();
                yield return null;
            }
        }
        else
        {
            var nextPlan = queue.Dequeue();

            if (nextPlan.IsExecutable)
            {
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
            }



            yield return StartCoroutine(PlayQueue(queue, OnQueueEnd, OnTurnEnd));
        }
    }

}
