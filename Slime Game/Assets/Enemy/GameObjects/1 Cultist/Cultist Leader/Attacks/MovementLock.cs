using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLock : BaseSkill
{
    public float timeBeforeSpawn;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == Player.instance.gameObject)
        {
            timeBeforeSpawn -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.instance.gameObject)
        {
            if(timeBeforeSpawn > 0)
            {
                owner.GetComponent<Animator>().Play("MovementPart2");
                Destroy(gameObject);
            }
        }
    }
}
