using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWait : BaseEnemy
{
    public float startWaitTime;
    float waitTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        waitTime = startWaitTime;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
