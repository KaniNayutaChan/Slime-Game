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

    public Vector2 stopMovingVector;

    int lastAttack = 0;
    int secondLastAttack = 1;

    public IdleType idleType;
    public enum IdleType
    {
        StayStill,
        MoveToPlayer,
        FlyToPlayer,
        MoveToRandomDestination
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timeTillAttack = Random.Range(minStartTimeTillAttack, maxStartTimeTillAttack);

        attackToUse = Random.Range(0, attackList.Length);
        while (attackToUse == lastAttack && attackToUse == secondLastAttack)
        {
            attackToUse = Random.Range(0, attackList.Length);
        }

        lastAttack = secondLastAttack;
        lastAttack = attackToUse;

        switch (idleType)
        {
            case IdleType.StayStill:
                SetDestinationEnemy(0, 0);
                FaceDestination();
                break;

            case IdleType.MoveToRandomDestination:
                float x = Random.Range(-maxXDestination, maxXDestination);
                float y = Random.Range(minYDestination, maxYDestination);
                SetDestination(x, y);
                FaceDestination();
                break;
        }

#if UNITY_EDITOR
        //attackToUse = 4;
#endif
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        switch(idleType)
        {
            case IdleType.MoveToPlayer:
                SetDestinationPlayerX();
                FacePlayer();
                break;
            case IdleType.FlyToPlayer:
                SetDestinationPlayer();
                FacePlayer();
                break;
        }

        StopMoving(stopMovingVector.x, stopMovingVector.y);
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
