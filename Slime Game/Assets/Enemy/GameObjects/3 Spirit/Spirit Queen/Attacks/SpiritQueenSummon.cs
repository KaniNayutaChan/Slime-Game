using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritQueenSummon : BaseEnemyAttack
{
    protected override void UseAttack()
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

        GameObject skill = Instantiate(skillPrefab, spawnPos, Quaternion.Euler(spawnRotation), RoomManager.instance.currentRoom.transform);
        skill.GetComponent<BaseEnemyHealth>().experience = 0;
        skill.GetComponent<BaseEnemyHealth>().maxHealth = 1;
    }
}
