using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAlert : BaseEnemy
{
    public AlertType alertType;
    public enum AlertType
    {
        Wander,
        StayStill,
        MoveToPlayer,
        FlyToPlayer,
        Run
    }

    public float maxXDistance;
    public float maxYDistance;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        switch (alertType)
        {
            case AlertType.Wander:
                float x = Random.Range(-maxXDistance, maxXDistance);
                float y = Random.Range(-maxYDistance, maxYDistance);
                SetDestinationEnemy(x, y);
                FaceDestination();
                break;

            case AlertType.StayStill:
                SetDestinationEnemy(0, 0);
                break;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!enemyHealth.IsInRange())
        {
            animator.Play("Wait");
        }

        FaceDestination();

        switch (alertType)
        {
            case AlertType.MoveToPlayer:
                SetDestinationPlayerX();
                break;

            case AlertType.FlyToPlayer:
                SetDestinationPlayer();
                break;

            case AlertType.Run:
                if(PlayerIsOnLeft())
                {
                    SetDestinationEnemy(100, 0);
                }
                else
                {
                    SetDestinationEnemy(-100, 0);
                }
                break;
        }
    }

}