using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistCharge : BaseEnemyAttack
{
    BaseEnemyHealth baseEnemyHealth;
    float startingHealth;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        baseEnemyHealth = animator.GetComponent<BaseEnemyHealth>();

        startingHealth = baseEnemyHealth.currentHealth;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(baseEnemyHealth.currentHealth < startingHealth)
        {
            animator.Play("Wait");
        }
    }
}
