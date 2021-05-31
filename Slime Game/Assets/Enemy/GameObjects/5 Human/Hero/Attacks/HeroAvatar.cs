using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAvatar : BaseEnemyAttack
{
    Hero hero;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        hero = animator.GetComponent<Hero>();
    }

    protected override void UseAttack()
    {
        hero.avatar = Instantiate(hero.avatarPrefab);
    }
}
