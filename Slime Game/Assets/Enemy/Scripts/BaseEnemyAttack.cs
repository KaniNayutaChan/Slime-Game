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

    public MoveType moveType;
    public enum MoveType
    {
        StayStill,
        MoveToPlayer,
        FlyToPlayer,
        MoveRelativeToEnemy
    }

    public SpawnType spawnType;
    public enum SpawnType
    {
        SpawnRelativeToEnemy,
        SpawnRelativeToPlayer,
        SpawnAtRandomPosition,
        SpawnInIncrements,
        SpawnInCircle
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

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
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(timeTillAttack > 0)
        {
            timeTillAttack -= Time.deltaTime;
        }
        else if(counter < noOfAttacks)
        {
            counter++;
            timeTillAttack = timeBetweenAttacks;
            UseAttack();
        }

        if(timeTillMove > 0)
        {
            timeTillMove -= Time.deltaTime;
        }
        else
        {
            MoveToDestination();
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
                SetSpawnPosIncrements(spawnVector.x, spawnVector.y, spawnVector.z);
                break;
            case SpawnType.SpawnInCircle:
                SetSpawnPosCircle(spawnVector.x);
                break;
        }

        GameObject skill = Instantiate(skillPrefab, spawnPos, Quaternion.Euler(spawnRotation), RoomManager.instance.currentRoom.transform);
        skill.GetComponent<BaseSkill>().owner = enemyPos.gameObject;
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

    protected void SetSpawnPosIncrements(float x, float y, float gapDistance)
    {
        if(IsFacingLeft())
        {
            spawnPos.Set(x - (counter * gapDistance), y, enemyPos.position.z);
        }
        else
        {
            spawnPos.Set(-x + (counter * gapDistance), y, enemyPos.position.z);
        }
    }

    protected void SetSpawnPosCircle(float gapDistance)
    {
        spawnPos.Set(enemyPos.position.x + (gapDistance * Mathf.Cos(2 * Mathf.PI * counter)/ noOfAttacks), enemyPos.position.y + (gapDistance * Mathf.Sin(2 * Mathf.PI * counter) / noOfAttacks), enemyPos.position.z);
    }
}
