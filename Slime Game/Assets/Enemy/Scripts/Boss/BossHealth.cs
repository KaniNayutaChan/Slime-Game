using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : BaseEnemyHealth
{
    public int bossNumber;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        base.Die();

        RoomManager.instance.rooms[RoomManager.instance.currentRoomNumber].enemies[number] = 0;
        RoomManager.instance.defeatedBosses[bossNumber] = true;
        PowerUpManager.instance.GrantPowerUp(bossNumber);
    }
}
