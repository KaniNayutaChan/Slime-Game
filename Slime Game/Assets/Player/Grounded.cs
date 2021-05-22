using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Arena"))
        {
            Player.instance.isGrounded = true;
            Player.instance.playerRB.velocity = Vector2.zero;
            Player.instance.hasAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            Player.instance.isGrounded = false;
        }
    }
}
