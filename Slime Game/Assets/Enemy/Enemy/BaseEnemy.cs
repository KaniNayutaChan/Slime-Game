using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachineBehaviour
{
    protected Transform playerPos;
    protected Vector3 destination;
    protected float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Player.instance.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    protected void FaceLeft()
    {

    }

    protected void FaceRight()
    {

    }

    protected void FacePlayer()
    {

    }

    protected void FaceDestination()
    {

    }

    protected void MoveToDestination()
    {

    }
}
