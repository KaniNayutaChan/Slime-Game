using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachineBehaviour
{
    protected Transform playerPos;
    protected Transform enemyPos;
    protected Vector3 destination;
    protected float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Player.instance.transform;
        enemyPos = animator.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    protected void FaceLeft()
    {
        enemyPos.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void FaceRight()
    {
        enemyPos.rotation = Quaternion.Euler(0, 180, 0);
    }

    protected void FacePlayer()
    {
        if(enemyPos.position.x > playerPos.position.x)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    protected void FaceDestination()
    {
        if(enemyPos.position.x > destination.x)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    protected void MoveToDestination()
    {
        enemyPos.position = Vector3.MoveTowards(enemyPos.position, destination, speed * Time.deltaTime);
    }
}
