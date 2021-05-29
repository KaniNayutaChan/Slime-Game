using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : StateMachineBehaviour
{
    protected Transform playerPos;
    protected Transform enemyPos;
    protected EnemyHealth enemyHealth;
    protected Vector3 destination;
    public float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Player.instance.transform;
        enemyPos = animator.transform;
        enemyHealth = animator.GetComponent<EnemyHealth>();
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

    protected bool IsFacingLeft()
    {
        if (enemyPos.rotation == Quaternion.Euler(0, 0, 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool PlayerIsOnLeft()
    {
        if (enemyPos.position.x > playerPos.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void FacePlayer()
    {
        if (PlayerIsOnLeft())
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
        if (enemyPos.position.x > destination.x)
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

    protected void SetDestinationEnemy(float x, float y)
    {
        if (!IsFacingLeft())
        {
            destination.Set(enemyPos.position.x + x, enemyPos.position.y + y, enemyPos.position.z);
        }
        else
        {
            destination.Set(enemyPos.position.x - x, enemyPos.position.y + y, enemyPos.position.z);
        }
    }

    protected bool IsAtDestination()
    {
        if (Vector3.Distance(enemyPos.position, destination) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void SetDestinationPlayerX()
    {
        destination.Set(playerPos.position.x, enemyPos.position.y, enemyPos.position.z);
    }

    protected void SetDestinationPlayer()
    {
        destination.Set(playerPos.position.x, playerPos.position.y, enemyPos.position.z);
    }
    protected void SetDestination(float x, float y)
    {
        destination.Set(x, y, enemyPos.position.z);
    }

    protected void SetDestinationX(float x)
    {
        destination.Set(x, enemyPos.position.y, enemyPos.position.z);
    }

    protected void SetDestinationY(float y)
    {
        destination.Set(enemyPos.position.x, y, enemyPos.position.z);
    }

    protected void StopMoving(float x, float y)
    {
        if(destination.x > x)
        {
            destination.x = x;
        }

        if(destination.x < -x)
        {
            destination.x = -x;
        }

        if(destination.y < y)
        {
            destination.y = y;
        }
    }
}