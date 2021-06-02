using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossIdle : BaseEnemy
{
    public int debugAttack;

    public string[] attackList;
    protected int attackToUse;

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

    BaseEnemyHealth baseEnemyHealth;
    int forcedAttackCounter = 0;
    public ForcedAttacks[] forcedAttacksList;
    [System.Serializable]
    public class ForcedAttacks
    {
        public string forcedAttackName;
        public float forcedAttackThreshHold;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        baseEnemyHealth = animator.GetComponent<BaseEnemyHealth>();

        timeTillAttack = Random.Range(minStartTimeTillAttack, maxStartTimeTillAttack);

        attackToUse = Random.Range(0, attackList.Length);

        while (attackToUse == lastAttack && attackToUse == secondLastAttack)
        {
            attackToUse = Random.Range(0, attackList.Length);
        }

        lastAttack = secondLastAttack;
        lastAttack = attackToUse;

        if (forcedAttackCounter < forcedAttacksList.Length)
        {
            if (baseEnemyHealth.currentHealth < forcedAttacksList[forcedAttackCounter].forcedAttackThreshHold * baseEnemyHealth.maxHealth)
            {
                animator.Play(forcedAttacksList[forcedAttackCounter].forcedAttackName);
                forcedAttackCounter++;
            }
        }

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
                StopMoving(stopMovingVector.x, stopMovingVector.y);
                FaceDestination();
                break;
        }

#if UNITY_EDITOR
        if (debugAttack < 7)
        {
            attackToUse = debugAttack;
        }
#endif
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        switch(idleType)
        {
            case IdleType.MoveToPlayer:
                SetDestinationPlayerX(0);
                StopMoving(stopMovingVector.x, stopMovingVector.y);
                FacePlayer();
                break;
            case IdleType.FlyToPlayer:
                SetDestinationPlayer();
                StopMoving(stopMovingVector.x, stopMovingVector.y);
                FacePlayer();
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
