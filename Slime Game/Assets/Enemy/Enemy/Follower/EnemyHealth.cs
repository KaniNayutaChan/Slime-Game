using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseEnemyHealth
{
    public float range;
    public float startAttackCooldown;
    float attackCooldown;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackCooldown = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(IsInRange() && attackCooldown < 0)
        {
            //animator.Play("Attack");
            attackCooldown = startAttackCooldown;
        }

        if(attackCooldown >= 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public bool IsInRange()
    {
        if(Vector2.Distance(transform.position, Player.instance.transform.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
