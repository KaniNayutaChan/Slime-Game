using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyIdle : BaseEnemy
{
    public IdleType idleType;
    
    public enum IdleType
    {
        StayStill,
        MoveInStraightLine,
        Wander
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}
