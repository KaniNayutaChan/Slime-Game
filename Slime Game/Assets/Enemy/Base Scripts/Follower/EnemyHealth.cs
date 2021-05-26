using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseEnemyHealth
{
    public float range;
    public float startAttackCooldown;
    float attackCooldown;
    public string[] attackList;
    int attackToUse;

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
            attackToUse = Random.Range(0, attackList.Length);
            attackCooldown = startAttackCooldown;

            if (!hasDied)
            {
                animator.Play(attackToUse);
            }
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
