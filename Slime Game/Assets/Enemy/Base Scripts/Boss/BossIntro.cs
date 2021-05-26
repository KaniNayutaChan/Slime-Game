using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : BaseEnemy
{
    public float minXTrigger;
    public float maxXTrigger;
    bool isAwakened;
    public float startTimeTillIdle;
    float timeTillIdle;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timeTillIdle = startTimeTillIdle;
        isAwakened = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(playerPos.position.x > minXTrigger && playerPos.position.x < maxXTrigger)
        {
            isAwakened = true;
        }

        if (isAwakened)
        {
            if (timeTillIdle > 0)
            {
                timeTillIdle -= Time.deltaTime;
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }
}
