using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossIdle : BaseEnemy
{
    public string[] attackList;
    int attackToUse;

    public float minStartTimeTillAttack;
    public float maxStartTimeTillAttack;
    float timeTillAttack;

    public float maxXDestination;
    public float minYDestination;
    public float maxYDestination;

    public IdleType idleType;
    public enum IdleType
    {
        MoveToPlayer,
        FlyToPlayer,
        MoveToRandomDestination
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        attackToUse = Random.Range(0, attackList.Length);
        timeTillAttack = Random.Range(minStartTimeTillAttack, maxStartTimeTillAttack);

        if(idleType == IdleType.MoveToRandomDestination)
        {
            float x = Random.Range(-maxXDestination, maxXDestination);
            float y = Random.Range(minYDestination, maxYDestination);
            SetDestination(x, y);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        switch(idleType)
        {
            case IdleType.MoveToPlayer:
                SetDestinationPlayerX();
                break;
            case IdleType.FlyToPlayer:
                SetDestinationPlayer();
                break;
        }

        MoveToDestination();

        if (timeTillAttack > 0)
        {
            timeTillAttack -= Time.deltaTime;
        }
        else
        {
            animator.Play(attackList[attackToUse]);
        }
    }
}
