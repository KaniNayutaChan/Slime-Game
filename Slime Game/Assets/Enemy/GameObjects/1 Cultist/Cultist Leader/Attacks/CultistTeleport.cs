using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistTeleport : BaseEnemyAttack
{
    protected override void UseAttack()
    {
        float xDestination = Random.Range(-movementVector.x, movementVector.x);
        SetDestination(xDestination, enemyPos.position.y);

        while(Vector3.Distance(Player.instance.transform.position, destination) < 1f)
        {
            xDestination = Random.Range(-movementVector.x, movementVector.x);
        }

        SetDestination(xDestination, enemyPos.position.y);
        enemyPos.position = destination;
    }
}
