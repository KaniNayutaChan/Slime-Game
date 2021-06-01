using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdle : BaseBossIdle
{
    protected Hero hero;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        hero = animator.GetComponent<Hero>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (hero.avatar != null)
        {
            hero.GetComponent<Animator>().Play(attackList[attackToUse]);
        }
    }
}
