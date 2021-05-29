using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireDarkness : BaseSkill
{
    bool playerIsOn = false;
    public GameObject darknessPrefab;

    private void OnDestroy()
    {
        if(!playerIsOn)
        {
            Instantiate(darknessPrefab, Player.instance.transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.instance.gameObject)
        {
            playerIsOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.instance.gameObject)
        {
            playerIsOn = false;
        }
    }
}
