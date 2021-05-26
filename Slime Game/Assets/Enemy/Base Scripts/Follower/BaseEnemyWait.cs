using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyWait : BaseEnemy
{
    public float minWaitTimeTillMove;
    public float maxWaitTimeTillMove;
    float timeTillMove;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        timeTillMove = Random.Range(minWaitTimeTillMove, maxWaitTimeTillMove);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (timeTillMove > 0)
        {
            timeTillMove -= Time.deltaTime;
        }
        else
        {
            animator.Play("Move");
        }
        
        if(enemyHealth.IsInRange())
        {
            animator.Play("Alert");
        }
    }
}
