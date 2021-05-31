using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAvatar : BaseHeroAttack
{
    protected override void UseAttack()
    {
        hero.avatar = Instantiate(hero.avatarPrefab);
    }
}
