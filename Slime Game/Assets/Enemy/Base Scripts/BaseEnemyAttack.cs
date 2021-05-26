using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttack : BaseEnemy
{
    public float startTimeTillMove;
    float timeTillMove;
    public float startTimeTillAttack;
    float timeTillAttack;
    public float timeBetweenAttacks;
    protected int counter;
    public int noOfAttacks;
    public GameObject skillPrefab;
    protected Vector3 spawnPos;
    public Vector3 spawnRotation;
    public Vector3 movementVector;
    public Vector3 spawnVector;
    public string nextAnimationName;
    public float startTimeTillNextAnimation;
    float timeTillNextAnimation;
    public bool startOnEnemy;
    public Vector2 stopMovingVector;

    public TransitionType transitionType;
    public enum TransitionType
    {
        AfterTime,
        ReachDestination
    }

    public MoveType moveType;
    public enum MoveType
    {
        StayStill,
        MoveToPlayer,
        FlyToPlayer,
        MoveRelativeToEnemy,
        MoveToDestination
    }

    public SpawnType spawnType;
    public enum SpawnType
    {
        NoSpawn,
        SpawnRelativeToEnemy,
        SpawnRelativeToPlayer,
        SpawnAtRandomPosition,
        SpawnInIncrements,
        SpawnInCircle
    }

    public SpawnTimingType spawnTimingType;
    public enum SpawnTimingType
    {
        SpawnAfterTime,
        SpawnAtDestination
    }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        timeTillNextAnimation = startTimeTillNextAnimation;
        timeTillAttack = startTimeTillAttack;
        timeTillMove = startTimeTillMove;
        counter = 0;
        FacePlayer();

        switch(moveType)
        {
            case MoveType.StayStill:
                SetDestinationEnemy(0, 0);
                break;
            case MoveType.MoveToPlayer:
                SetDestinationPlayerX();
                break;
            case MoveType.FlyToPlayer:
                SetDestinationPlayer();
                break;
            case MoveType.MoveRelativeToEnemy:
                SetDestinationEnemy(movementVector.x, movementVector.y);
                break;
            case MoveType.MoveToDestination:
                if(IsFacingLeft())
                {
                    SetDestinationX(movementVector.x);
                }
                else
                {
                    SetDestinationX(movementVector.y);
                }
                break;
        }

        StopMoving(stopMovingVector.x, stopMovingVector.y);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (spawnTimingType == SpawnTimingType.SpawnAfterTime)
        {
            if (timeTillAttack > 0)
            {
                timeTillAttack -= Time.deltaTime;
            }
            else if (counter < noOfAttacks)
            {
                timeTillAttack = timeBetweenAttacks;
                UseAttack();
                counter++;
            }
        }

        if(spawnTimingType == SpawnTimingType.SpawnAtDestination)
        {
            if(IsAtDestination())
            {
                if (timeTillAttack > 0)
                {
                    timeTillAttack -= Time.deltaTime;
                }
                else if (counter < noOfAttacks)
                {
                    timeTillAttack = timeBetweenAttacks;
                    UseAttack();
                    counter++;
                }
            }
        }

        if(timeTillMove > 0)
        {
            timeTillMove -= Time.deltaTime;
        }
        else
        {
            MoveToDestination();
        }

        switch(transitionType)
        {
            case TransitionType.AfterTime:
                if (timeTillNextAnimation > 0)
                {
                    timeTillNextAnimation -= Time.deltaTime;
                }
                else
                {
                    animator.Play(nextAnimationName);
                }
                break;

            case TransitionType.ReachDestination:
                if(IsAtDestination())
                {
                    if (timeTillNextAnimation > 0)
                    {
                        timeTillNextAnimation -= Time.deltaTime;
                    }
                    else
                    {
                        animator.Play(nextAnimationName);
                    }
                }
                break;
        }
    }

    protected virtual void UseAttack()
    {
        switch (spawnType)
        {
            case SpawnType.SpawnRelativeToEnemy:
                SetSpawnPosEnemy(spawnVector.x, spawnVector.y);
                break;
            case SpawnType.SpawnRelativeToPlayer:
                SetSpawnPosPlayer(spawnVector.x, spawnVector.y);
                break;
            case SpawnType.SpawnAtRandomPosition:
                SetRandomSpawnPos(spawnVector.x, spawnVector.y, spawnVector.z);
                break;
            case SpawnType.SpawnInIncrements:
                SetSpawnPosIncrements(spawnVector.x, spawnVector.y, spawnVector.z, startOnEnemy);
                break;
            case SpawnType.SpawnInCircle:
                SetSpawnPosCircle(spawnVector.x);
                break;
        }

        if (spawnType != SpawnType.NoSpawn)
        {
            GameObject skill = Instantiate(skillPrefab, spawnPos, Quaternion.Euler(spawnRotation), RoomManager.instance.currentRoom.transform);
            skill.GetComponent<BaseSkill>().owner = enemyPos.gameObject;
        }
    }

    protected void SetSpawnPosEnemy(float x, float y)
    {
        if(IsFacingLeft())
        {
            spawnPos.Set(enemyPos.position.x - x, enemyPos.position.y + y, enemyPos.position.z);
        }
        else
        {
            spawnPos.Set(enemyPos.position.x + x, enemyPos.position.y + y, enemyPos.position.z);
        }
    }

    protected void SetSpawnPosPlayer(float x, float y)
    {
        spawnPos.Set(playerPos.position.x + x, playerPos.position.y + y, enemyPos.position.z);
    }

    protected void SetRandomSpawnPos(float x, float yMin, float yMax)
    {
        float randX = Random.Range(-x, x);
        float randY = Random.Range(yMin, yMax);
        spawnPos.Set(randX, randY, enemyPos.position.z);
    }

    protected void SetSpawnPosIncrements(float x, float y, float gapDistance, bool startOnEnemy)
    {
        if (startOnEnemy)
        {
            if (IsFacingLeft())
            {
                spawnPos.Set(enemyPos.position.x + x - (counter * gapDistance), enemyPos.position.y, enemyPos.position.z);
            }
            else
            {
                spawnPos.Set(enemyPos.position.x - x + (counter * gapDistance), enemyPos.position.y, enemyPos.position.z);
            }
        }
        else
        {
            if (IsFacingLeft())
            {
                spawnPos.Set(x - (counter * gapDistance), y, enemyPos.position.z);
            }
            else
            {
                spawnPos.Set(-x + (counter * gapDistance), y, enemyPos.position.z);
            }
        }
    }

    protected void SetSpawnPosCircle(float gapDistance)
    {
        spawnPos.Set(enemyPos.position.x + (gapDistance * Mathf.Cos(2 * Mathf.PI * counter)/ noOfAttacks), enemyPos.position.y + (gapDistance * Mathf.Sin(2 * Mathf.PI * counter) / noOfAttacks), enemyPos.position.z);
    }
}
