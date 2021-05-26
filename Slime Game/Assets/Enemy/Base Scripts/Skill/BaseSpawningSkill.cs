using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawningSkill : BaseSkill
{
    public GameObject skillPrefab;
    public float timeTillSpawn;

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
        else
        {
            GameObject skill = Instantiate(skillPrefab, transform.position, transform.rotation);
            skill.GetComponent<BaseSkill>().owner = this.gameObject;
            Destroy(gameObject);
        }
    }
}
