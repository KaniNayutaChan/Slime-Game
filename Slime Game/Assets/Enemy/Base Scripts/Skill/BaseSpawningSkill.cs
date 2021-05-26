using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawningSkill : BaseSkill
{
    public GameObject skillPrefab;
    public float timeTillSpawn;
    bool hasSpawnedSkill = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if(timeTillSpawn > 0)
        {
            timeTillSpawn -= Time.deltaTime;
        }
        else if(!hasSpawnedSkill)
        {
            hasSpawnedSkill = true;
            Instantiate(skillPrefab, transform.position, transform.rotation, RoomManager.instance.currentRoom.transform);
            Destroy(gameObject);
        }
    }
}
