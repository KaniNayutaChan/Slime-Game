using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : BaseEnlargenSkill
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.instance.gameObject)
        {
            Player.instance.canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.instance.gameObject)
        {
            Player.instance.canMove = true;
        }
    }
}
