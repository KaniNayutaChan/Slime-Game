using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyIdle : BaseEnemy
{
    public IdleType idleType;
    public enum IdleType
    {
        Wander,
        StayStill,
        MoveInStraightLine,
    }

    public float maxXDistance;
    public float maxYDistance;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        switch (idleType)
        {
            case IdleType.Wander:
                float x = Random.Range(-maxXDistance, maxXDistance);
                float y = Random.Range(-maxYDistance, maxYDistance);
                SetDestinationEnemy(x, y);
                FaceDestination();
                break;

            case IdleType.StayStill:
                SetDestinationEnemy(0,0);
                break;

            case IdleType.MoveInStraightLine:
                if(IsFacingLeft())
                {
                    SetDestinationEnemy(-100, 0);
                }
                else
                {
                    SetDestinationEnemy(100, 0);
                }
                break;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        MoveToDestination();

        if(IsAtDestination())
        {
            if (idleType != IdleType.StayStill)
            {
                animator.Play("Wait");
            }
        }

        if(enemyHealth.IsInRange())
        {
            animator.Play("Alert");
        }
    }
}
